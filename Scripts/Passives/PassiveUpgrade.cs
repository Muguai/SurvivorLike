using System;
using System.Collections.Generic;
using Godot;
using System.Reflection;
public partial class PassiveUpgrade : GodotObject
{
    public float SizeMultipler { get; set; } = 0.0f;
    public float ExplosionSizeMultipler { get; set; } = 0.0f;
    public float SpeedMultipler { get; set; } = 0.0f;
    public float LifeStealMulitplier { get; set; } = 0.0f;
    public float LifeStealFlat { get; set; } = 0.0f;
    public float CriticalDamageChanceFlat { get; set; } = 0.0f;
    public float CriticalDamageMultiplier { get; set; } = 0.0f;
    public float KnockbackMultiplier { get; set; } = 0.0f;
    public float KnockbackFlat { get; set; } = 0.0f;
    public float DamageMultipler { get; set; } = 0.0f;
    public float DamageFlat { get; set; } = 0.0f;
    public List<PassiveDelegate> Functions {get; set;} = new List<PassiveDelegate>();


    public void AddFunction(List<PassiveDelegate> _functions)
    {
        foreach (var function in _functions)
        {
            Functions.Add(function);
        }
    }

    public void RemoveFunctions(List<PassiveDelegate> _functions)
    {
        foreach (var function in _functions)
        {
            if (Functions.Contains(function))
                Functions.Remove(function);
        }
    }

    public void ExecuteFunctions(Node node)
    {
        foreach (var function in Functions)
        {
            function.Invoke(node);
        }
    }

    public void AddUpgrades(PassiveUpgrade upgrade)
    {
        SizeMultipler += upgrade.SizeMultipler;
        ExplosionSizeMultipler += upgrade.ExplosionSizeMultipler;
        SpeedMultipler += upgrade.SpeedMultipler;
        LifeStealMulitplier += upgrade.LifeStealMulitplier;
        LifeStealFlat += upgrade.LifeStealFlat;
        CriticalDamageMultiplier += upgrade.CriticalDamageMultiplier;
        KnockbackMultiplier += upgrade.KnockbackMultiplier;
        KnockbackFlat += upgrade.KnockbackFlat;
        DamageMultipler += upgrade.DamageMultipler;
        DamageFlat += upgrade.DamageFlat;
        CriticalDamageChanceFlat += upgrade.CriticalDamageChanceFlat;
        AddFunction(upgrade.Functions);
    }

    public void RemoveUpgrades(PassiveUpgrade upgrade)
    {
        SizeMultipler -= upgrade.SizeMultipler;
        ExplosionSizeMultipler -= upgrade.ExplosionSizeMultipler;
        SpeedMultipler -= upgrade.SpeedMultipler;
        LifeStealMulitplier -= upgrade.LifeStealMulitplier;
        LifeStealFlat -= upgrade.LifeStealFlat;
        CriticalDamageMultiplier -= upgrade.CriticalDamageMultiplier;
        KnockbackMultiplier -= upgrade.KnockbackMultiplier;
        KnockbackFlat -= upgrade.KnockbackFlat;
        DamageMultipler -= upgrade.DamageMultipler;
        DamageFlat -= upgrade.DamageFlat;
        CriticalDamageChanceFlat -= upgrade.CriticalDamageChanceFlat;
        RemoveFunctions(upgrade.Functions);
    }

    public bool IsEmpty()
    {
        PropertyInfo[] properties = GetType().GetProperties();
        foreach (var property in properties)
        {
            object value = property.GetValue(this);
            if (value != null && !value.Equals(Activator.CreateInstance(property.PropertyType)))
            {
                return false;
            }
        }
        if (Functions.Count > 0)
        {
            return false;
        }
        return true;
    }
}
