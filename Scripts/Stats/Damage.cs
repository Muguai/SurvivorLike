using Godot;
using System;
using System.Collections.Generic;

public class Damage
{
	public float Amount { get; set; }
	public float Knockback { get; set; }
	public Godot.Collections.Array<DamageType> Types { get; set; }
	public DamageSource Source { get; set; }
	public float LifeStealPercentage { get; set; }
	public float CriticalDamageMultiplier { get; set; }
	public bool IsCritical { get; set; }
	public float ArmorReduceValue { get; set; } = 0.5f;
	public Dictionary<DamageType, float> DamageTypeMultipliers { get; set; }


	public Damage(Stats stat = null)
	{
		stat = stat == null ? new Stats() : stat;

		this.Knockback = stat.Knockback;
		this.Types = stat.Types;
		this.IsCritical = IsCriticalHit(stat.CriticalDamageMultiplier - 1);
		this.LifeStealPercentage = stat.LifeStealPercentage;
		this.Source = stat.Source;
		this.DamageTypeMultipliers = stat.DamageTypeMultipliers;
		
		this.Amount = CalculateFinalDamage(stat.Damage);
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

		foreach (DamageType type in Types)
		{
			finalDamage *= DamageTypeMultipliers[type];
		}

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
