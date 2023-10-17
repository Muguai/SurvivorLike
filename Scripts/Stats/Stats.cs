using Godot;

public partial class Stats : Node
{
	[Export]
	public float Damage { get; set; }
	[Export]
	public float Knockback { get; set; }
	[Export]
	public Godot.Collections.Array<DamageType> Types { get; set; }
	[Export]
	public Godot.Collections.Array<DamageSource> Sources { get; set; }
	[Export]
	public float LifeStealPercentage { get; set; }
	[Export]
	public float CriticalDamageMultiplier { get; set; }
	[Export]
	public float CritChance { get; set; } = 0.0f;

	[Export]
	public float Speed { get; set; } = 300.0f;
	/*
	public Stats()
	{
		GD.Print("WTF");
		Damage = 0f;
		Knockback = 0f;
		Types = new Godot.Collections.Array<DamageType>();
		Sources = new Godot.Collections.Array<DamageSource>();
		LifeStealPercentage = 0f;
		CriticalDamageMultiplier = 1f;
		CritChance = 0f;
		Speed = 0f;
	}
	*/


	public void AddBonus(Stats bonusStats)
	{
		Damage += bonusStats.Damage;
		Knockback += bonusStats.Knockback;
		LifeStealPercentage += bonusStats.LifeStealPercentage;
		if(bonusStats.CriticalDamageMultiplier > 1){
			CriticalDamageMultiplier += bonusStats.CriticalDamageMultiplier - 1;
		}
		CritChance += bonusStats.CritChance;
		Speed += bonusStats.Speed;

		foreach (var type in bonusStats.Types)
		{
			if (!Types.Contains(type))
			{
				Types.Add(type);
			}
		}

		foreach (var source in bonusStats.Sources)
		{
			if (!Sources.Contains(source))
			{
				Sources.Add(source);
			}
		}

	}

	public override string ToString()
	{
		return $"Damage: {Damage}, " +
			   $"Knockback: {Knockback}, " +
			   $"Types: [{string.Join(", ", Types)}], " +
			   $"Sources: [{string.Join(", ", Sources)}], " +
			   $"LifeStealPercentage: {LifeStealPercentage}, " +
			   $"CriticalDamageMultiplier: {CriticalDamageMultiplier}, " +
			   $"CritChance: {CritChance}, " +
			   $"Speed: {Speed}";
	}
}
