using Godot;
using System;
using System.Linq;

public partial class GenericSpell
{
	private Node spellNode;
	private Stats stats;
	private PassiveUpgrade passiveUpgrade;

	public void InitializeSpell(Node _spellNode, Stats _stats, PassiveUpgrade _passiveUpgrade)
	{
		spellNode = _spellNode;
		stats = _stats;
		passiveUpgrade = _passiveUpgrade;

		if (passiveUpgrade.SizeMultipler > 0)
			ApplySizeMultiplier();

		if (passiveUpgrade.ExplosionSizeMultipler > 0)
			ApplyExplosionMultiplier();
		
		stats.Speed *= 1 + passiveUpgrade.SpeedMultipler;
		stats.Knockback = passiveUpgrade.KnockbackFlat;
		stats.Knockback *= 1 + passiveUpgrade.KnockbackMultiplier;
		stats.LifeStealPercentage += passiveUpgrade.LifeStealFlat;
		stats.LifeStealPercentage *=  1 + passiveUpgrade.LifeStealMulitplier;
		stats.CritChance += passiveUpgrade.CriticalDamageChanceFlat;
		stats.CriticalDamageMultiplier += passiveUpgrade.CriticalDamageMultiplier;
		stats.Damage += passiveUpgrade.DamageFlat;
		stats.Damage *= 1+ passiveUpgrade.DamageMultipler;
	}

	private void ApplySizeMultiplier()
	{
		Godot.Collections.Array<Node> children = spellNode.GetChildren();
		
		foreach (Node child in children)
		{
			if (child.IsInGroup("Size"))
			{
				if (child is GpuParticles2D particle)
				{
					ParticleProcessMaterial mat = particle.ProcessMaterial as ParticleProcessMaterial;
					mat.ScaleMin *= 1 + passiveUpgrade.SizeMultipler; 
				}
				else if (child is Area2D area)
				{
					area.Scale *= 1 + passiveUpgrade.SizeMultipler;
				}else if(child is Sprite2D sprite){
					sprite.Scale *= 1 + passiveUpgrade.SizeMultipler;
				}
			}
		}
	}

	private void ApplyExplosionMultiplier()
	{
		Godot.Collections.Array<Node> children = spellNode.GetChildren();

		foreach (Node child in children)
		{
			if (child.IsInGroup("Explosion"))
			{
				if (child is GpuParticles2D particle)
				{
					particle.Amount = Mathf.RoundToInt(particle.Amount * (1 + passiveUpgrade.ExplosionSizeMultipler));
				}
				else if (child is Area2D area)
				{
					area.Scale *= 1 + passiveUpgrade.ExplosionSizeMultipler;
				}
			}
		}
	}

}
