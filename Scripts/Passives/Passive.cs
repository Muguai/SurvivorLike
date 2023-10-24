using Godot;
using System.Collections.Generic;

public partial class Passive : Resource
{

    public string PassiveText { get; set; } = "This is a Passive";

    public Dictionary<string, PassiveUpgrade> passiveUpgrades { get; set; } = new Dictionary<string, PassiveUpgrade>();

    public void AddPassive()
    {
        foreach (KeyValuePair<string, PassiveUpgrade> pu in passiveUpgrades)
        {
            PassiveSystem.Instance.AddPassiveUpgrade(pu.Key, pu.Value);
        }
    }

    
    public void RemovePassive()
    {
        foreach (KeyValuePair<string, PassiveUpgrade> pu in passiveUpgrades)
        {
            PassiveSystem.Instance.RemovePassiveUpgrade(pu.Key, pu.Value);
        }
    }


    public bool ConnectToSignalRecursively(Node node, string signalName, Godot.Callable callable)
    {
        if (node.HasSignal(signalName))
        {
            node.Connect(signalName, callable);
            return true;
        }

        foreach (Node child in node.GetChildren())
        {
            if (ConnectToSignalRecursively(child, signalName, callable))
            {
                return true;
            }
        }

        return false;
    }

}