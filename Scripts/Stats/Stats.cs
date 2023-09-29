using Godot;
using System;
using System.Collections.Generic;

public partial class Stats : Node
{
	[Export]
	public float Damage { get; set; }
	[Export]
	public float Knockback { get; set; }
	[Export]
	public Godot.Collections.Array<DamageType> Types { get; set; }
	[Export]
	public DamageSource Source { get; set; }
	[Export]
	public float LifeStealPercentage { get; set; }
	[Export]
	public float CriticalDamageMultiplier { get; set; }
	public Dictionary<DamageType, float> DamageTypeMultipliers {get;set;}


	public Stats(float damage = 0f, float knockback = 0f, Godot.Collections.Array<DamageType> types = null, DamageSource source = DamageSource.None, float lifeStealPercentage = 0f, float criticalDamageMultiplier = 1f)
	{
		Damage = damage;
		Knockback = knockback;
		Types = types ?? new Godot.Collections.Array<DamageType>();
		Source = source;
		LifeStealPercentage = lifeStealPercentage;
		CriticalDamageMultiplier = criticalDamageMultiplier;
	
		DamageTypeMultipliers = new();
		foreach (DamageType type in Enum.GetValues(typeof(DamageType)))
		{
			DamageTypeMultipliers[type] = 1.0f;
		}
	}

	 public Stats()
	{
		Damage = 0f;
		Knockback = 0f;
		Types = new Godot.Collections.Array<DamageType>();
		Source = DamageSource.None;
		LifeStealPercentage = 0f;
		CriticalDamageMultiplier = 1f;
		DamageTypeMultipliers = new();
		foreach (DamageType type in Enum.GetValues(typeof(DamageType)))
		{
			DamageTypeMultipliers[type] = 1.0f;
		}
	}

	public void SetDamageTypeMultiplier(DamageType type, float multiplier)
	{
		DamageTypeMultipliers[type] = multiplier;
	}

	public float GetDamageTypeMultiplier(DamageType type)
	{
		return DamageTypeMultipliers[type];
	}


	public void AddBonus(Stats bonusStats)
	{
		Damage += bonusStats.Damage;
		Knockback += bonusStats.Knockback;
		LifeStealPercentage += bonusStats.LifeStealPercentage;
		CriticalDamageMultiplier += bonusStats.CriticalDamageMultiplier - 1;

		foreach (var type in bonusStats.Types)
		{
			if (!Types.Contains(type))
			{
				Types.Add(type);
			}
		}

		if (Source == DamageSource.None)
		{
			Source = bonusStats.Source;
		}
	}
}
