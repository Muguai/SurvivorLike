using Godot;
using System;
using System.Collections.Generic;

public partial class PassiveSystem : Node
{
    private static PassiveSystem _instance;

    public static PassiveSystem Instance
    {
        get { return _instance; }
    }

    private Dictionary<string, PassiveUpgrade> passiveUpgrades = new Dictionary<string, PassiveUpgrade>();
    private List<Passive> passives = new List<Passive>();

    public override void _Ready()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            QueueFree();
        }

        /*
        string folderPath = "res://Scripts/Passives/PassiveResources/";
        string[] files = Directory.GetFiles(folderPath);

        foreach (string file in files)
        {
            Resource resource = GD.Load(file) as Resource;

            if (resource is Passive passiveScript)
            {
                Passive passiveInstance = (Passive)Activator.CreateInstance(passiveScript.GetType());

                AddPassive(passiveInstance);
            }
        }

        foreach(Passive p in GetRandomUniquePassives(3)){
            GD.Print(p.ResourceName);
        }
        */
    }

    public void AddPassiveUpgrade(string key, PassiveUpgrade passiveUpgrade)
    {
        if (passiveUpgrades.ContainsKey(key))
        {
            PassiveUpgrade upgrades = passiveUpgrades[key];
            upgrades.AddUpgrades(passiveUpgrade);
            passiveUpgrades[key] = upgrades;
        }
        else
        {
            passiveUpgrades[key] = passiveUpgrade;
        }
    }

    public void RemovePassiveUpgrade(string key, PassiveUpgrade passiveUpgrade)
    {
        if (passiveUpgrades.TryGetValue(key, out PassiveUpgrade upgrades))
        {
            upgrades.RemoveUpgrades(passiveUpgrade);

            if (upgrades.IsEmpty())
            {
                passiveUpgrades.Remove(key);
            }
            else
            {
                passiveUpgrades[key] = upgrades;
            }
        }
    }

    public PassiveUpgrade GetPassiveUpgrade(string key)
    {
        PassiveUpgrade passiveUpgrade = new PassiveUpgrade();

        if (passiveUpgrades.TryGetValue(key, out PassiveUpgrade value))
        {
            passiveUpgrade.AddUpgrades(value);
        }

        return passiveUpgrade;
    }

    public void AddPassive(Passive passive)
    {
        passives.Add(passive);
    }

    public List<Passive> GetRandomUniquePassives(int count)
    {


        Random random = new Random();
        List<Passive> availablePassives = new List<Passive>(passives);

        if (count > availablePassives.Count)
        {
            GD.Print("Passives is less than " + count + " long");
            return availablePassives;
        }

        List<Passive> selectedPassives = new List<Passive>();
        count = Mathf.Clamp(count, 0, availablePassives.Count);

        while (selectedPassives.Count < count && availablePassives.Count > 0)
        {
            int randomIndex = random.Next(0, availablePassives.Count);
            Passive selectedPassive = availablePassives[randomIndex];

            if (!selectedPassives.Contains(selectedPassive))
            {
                selectedPassives.Add(selectedPassive);
            }

            availablePassives.RemoveAt(randomIndex);
        }

        return selectedPassives;
    }

}
