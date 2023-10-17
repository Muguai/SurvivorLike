using Godot;
using System;
using System.Collections.Generic;

public partial class PlayerStats : Stats
{
    [Export]
    public int Level { get; set; } = 1;

    public void AddBonusPlayer(PlayerStats bonusStats)
    {
        Level += bonusStats.Level;
    }

    public override void _Ready()
	{
		 EventManager.Instance.Connect("OnLevelUp", Callable.From(() => {
            Level += 1;
            GD.Print("Player is Level: " + Level);
		}));
		
	}

    public override string ToString()
    {
        return $"Level: {Level}, " +
               $"Damage: {Damage}, " +
               $"Knockback: {Knockback}, " +
               $"Types: [{string.Join(", ", Types)}], " +
               $"Sources: [{string.Join(", ", Sources)}], " +
               $"LifeStealPercentage: {LifeStealPercentage}, " +
               $"CriticalDamageMultiplier: {CriticalDamageMultiplier}, " +
               $"CritChance: {CritChance}, " +
               $"Speed: {Speed}";
    }
}
