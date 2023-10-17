
using Godot;
using Utils;
namespace SpellUtils;

public static class InitializeSpell
{
    public static void Initialize(Node spellNode, Stats stats )
    {

        PassiveUpgrade final = new PassiveUpgrade();
        PassiveSystem passiveSystem = PassiveSystem.Instance;

        foreach (DamageSource ds in stats.Sources)
        {
            final.AddUpgrades(passiveSystem.GetPassiveUpgrade(EnumHelper.EnumToString(ds)));
        }

        foreach (DamageType dt in stats.Types)
        {
            final.AddUpgrades(passiveSystem.GetPassiveUpgrade(EnumHelper.EnumToString(dt)));
        }

        final.AddUpgrades(passiveSystem.GetPassiveUpgrade(spellNode.Name));

        ApplyUpgrade(spellNode, stats, final);
        
    }

    private static void ApplyUpgrade(Node spellNode, Stats stats, PassiveUpgrade upgrade)
    {
        if (upgrade.SizeMultipler > 0)
            ApplySizeMultiplier(spellNode, upgrade);

        if (upgrade.ExplosionSizeMultipler > 0)
            ApplyExplosionMultiplier(spellNode, upgrade);

        stats.Speed *= 1 + upgrade.SpeedMultipler;
        stats.Knockback = upgrade.KnockbackFlat;
        stats.Knockback *= 1 + upgrade.KnockbackMultiplier;
        stats.LifeStealPercentage += upgrade.LifeStealFlat;
        stats.LifeStealPercentage *= 1 + upgrade.LifeStealMulitplier;
        stats.CritChance += upgrade.CriticalDamageChanceFlat;
        stats.CriticalDamageMultiplier += upgrade.CriticalDamageMultiplier;
        stats.Damage += upgrade.DamageFlat;
        stats.Damage *= 1 + upgrade.DamageMultipler;
    }

    private static void ApplySizeMultiplier(Node spellNode, PassiveUpgrade upgrade)
    {
        Godot.Collections.Array<Node> children = spellNode.GetChildren();

        foreach (Node child in children)
        {
            if (child.IsInGroup("Size"))
            {
                if (child is GpuParticles2D particle)
                {
                    ParticleProcessMaterial mat = particle.ProcessMaterial as ParticleProcessMaterial;
                    mat.ScaleMin *= 1 + upgrade.SizeMultipler;
                }
                else if (child is Area2D area)
                {
                    area.Scale *= 1 + upgrade.SizeMultipler;
                }
                else if (child is Sprite2D sprite)
                {
                    sprite.Scale *= 1 + upgrade.SizeMultipler;
                }
            }
        }
    }

    private static void ApplyExplosionMultiplier(Node spellNode, PassiveUpgrade upgrade)
    {
        Godot.Collections.Array<Node> children = spellNode.GetChildren();

        foreach (Node child in children)
        {
            if (child.IsInGroup("Explosion"))
            {
                if (child is GpuParticles2D particle)
                {
                    particle.Amount = Mathf.RoundToInt(particle.Amount * (1 + upgrade.ExplosionSizeMultipler));
                }
                else if (child is Area2D area)
                {
                    area.Scale *= 1 + upgrade.ExplosionSizeMultipler;
                }
            }
        }
    }
}
