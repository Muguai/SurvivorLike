public partial class BoomBoom : Passive
{

  public BoomBoom() {
    PassiveUpgrade passiveUpgrade = new();
    passiveUpgrade.ExplosionSizeMultipler = 0.2f;
    passiveUpgrades.Add("Fire", passiveUpgrade);
  }

}