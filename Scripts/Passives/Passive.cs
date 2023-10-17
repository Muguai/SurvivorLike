using Godot;
using System.Collections.Generic;

public partial class Passive : Resource{

    public string PassiveText {get; set;} = "This is a Passive";

    public Dictionary<string, PassiveUpgrade> passiveUpgrades { get; set; } = new Dictionary<string, PassiveUpgrade>();

    public void AddPassive(){
        foreach(KeyValuePair<string, PassiveUpgrade> pu in passiveUpgrades){
            PassiveSystem.Instance.AddPassiveUpgrade(pu.Key, pu.Value);
        }
    }
}