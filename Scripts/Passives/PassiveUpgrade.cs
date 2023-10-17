using System;
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
        return true;
    }
}
