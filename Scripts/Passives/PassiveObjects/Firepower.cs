using Godot;

public partial class Firepower : Passive
{
  public Firepower() {
    //Increase Fire Attacks Damage by 20%
    PassiveUpgrade passiveUpgrade = new();
    passiveUpgrade.DamageMultipler = 0.2f;
    passiveUpgrades.Add(passiveUpgrade);
    PassiveSystem.Instance.AddPassiveUpgrade(DamageType.Fire, passiveUpgrade);
  }
}