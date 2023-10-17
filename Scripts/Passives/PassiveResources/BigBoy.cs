
public partial class BigBoy : Passive
{

  public BigBoy() {
    PassiveUpgrade passiveUpgrade = new();
    passiveUpgrade.SizeMultipler = 0.2f;
    passiveUpgrades.Add("FireBall", passiveUpgrade);
  }

}