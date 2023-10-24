using Godot;
public partial class Firepower : Passive
{

  public Firepower()
  {
    PassiveUpgrade passiveUpgrade = new();
    passiveUpgrade.DamageMultipler = 0.2f;
    passiveUpgrade.Functions.Add(CustomMethod);
    passiveUpgrades.Add("Fire", passiveUpgrade);
    PassiveText = "Increase Fire Attacks Damage by 20%";
  }

  public void CustomMethod(Node node)
  {
    ConnectToSignalRecursively(node, "HitboxHit", Callable.From(() => 
      _OnSpellHits()
    ));
  }

  public void _OnSpellHits()
  {
    GD.Print("Spell Hits signal received!");
  }


}