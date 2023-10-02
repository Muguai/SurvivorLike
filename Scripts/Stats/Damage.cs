using Godot;
using System;
using System.Collections.Generic;

public class Damage
{
	public float Amount { get; set; }
	public float Knockback { get; set; }
	public Godot.Collections.Array<DamageType> Types { get; set; }
	public Godot.Collections.Array<DamageSource> Sources { get; set; }
	public float LifeStealPercentage { get; set; }
	public float CriticalDamageMultiplier { get; set; }
	public bool IsCritical { get; set; }
	public float ArmorReduceValue { get; set; } = 0.5f;
	public Godot.Collections.Dictionary<DamageType, float> DamageTypeMultipliers { get; set; }


	public Damage(Stats stat)
	{

		Knockback = stat.Knockback;
		Types = stat.Types;
		CriticalDamageMultiplier = stat.CriticalDamageMultiplier;
		IsCritical = IsCriticalHit(stat.CritChance);
		LifeStealPercentage = stat.LifeStealPercentage;
		Sources = stat.Sources;
		
		Amount = CalculateFinalDamage(stat.Damage);
	}

	private bool IsCriticalHit(float criticalChance)
	{
		Random random = new Random();
		float randomValue = (float)random.NextDouble();
		return randomValue <= criticalChance;
	}

	public void CalculateDamageReduction(float armorValue)
	{
		float damageReduction = armorValue * ArmorReduceValue;
		Amount -= damageReduction;

		if (Amount < 0)
		{
			Amount = 0;
		}
	}


	public float CalculateFinalDamage(float baseDamage)
	{
		float finalDamage = baseDamage;
		
		if (IsCritical)
		{
			finalDamage *= CriticalDamageMultiplier;
		}

		return finalDamage;
	}

	public float LifeSteal(float totalDamage)
	{
		float lifeStealAmount = totalDamage * LifeStealPercentage;
		return lifeStealAmount;
	}

	public override string ToString()
	{
		return $"Amount: {Amount}, Knockback: {Knockback}, Type: {string.Join(", ", Types)}, Critical: {IsCritical}";
	}
}
