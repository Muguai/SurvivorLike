
public partial class Firepower : Passive
{
  
  public Firepower() {
    PassiveUpgrade passiveUpgrade = new();
    passiveUpgrade.DamageMultipler = 0.2f;
    passiveUpgrades.Add("Fire", passiveUpgrade);

    PassiveText = "Increase Fire Attacks Damage by 20%";
  }

}