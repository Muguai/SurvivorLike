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

    private Dictionary<DamageType, PassiveUpgrade> passiveUpgradesByDamageType = new();
    private Dictionary<DamageSource, PassiveUpgrade> passiveUpgradesByDamageSource = new();
    private Dictionary<string, PassiveUpgrade> passiveUpgradesBySpellName = new();
    
    //List of Passives
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


        PassiveUpgrade tempP = new PassiveUpgrade();
        tempP.ExplosionSizeMultipler = 0.5f;
        PassiveSystem.Instance.AddPassiveUpgrade("Fireball", tempP);
    }

    public void AddPassiveUpgrade<T>(Dictionary<T, PassiveUpgrade> upgradesDictionary, T key, PassiveUpgrade passiveUpgrade)
    {
        if (upgradesDictionary.ContainsKey(key))
        {
            PassiveUpgrade upgrades = upgradesDictionary[key];
            upgrades.AddUpgrades(passiveUpgrade);
            upgradesDictionary[key] = upgrades;
        }
        else
        {
            PassiveUpgrade upgrades = passiveUpgrade;
            upgradesDictionary[key] = upgrades;
        }
    }

    public void AddPassiveUpgrade(string spellName, PassiveUpgrade passiveUpgrade)
    {
        AddPassiveUpgrade(passiveUpgradesBySpellName, spellName, passiveUpgrade);
    }

    public void AddPassiveUpgrade(DamageType type, PassiveUpgrade passiveUpgrade)
    {
        AddPassiveUpgrade(passiveUpgradesByDamageType, type, passiveUpgrade);
    }

    public void AddPassiveUpgrade(DamageSource source, PassiveUpgrade passiveUpgrade)
    {
        AddPassiveUpgrade(passiveUpgradesByDamageSource, source, passiveUpgrade);
    }

    public void AddPassive(Passive passive){
        passives.Add(passive);
    }

    public PassiveUpgrade GetPassiveUpgrade(string spellName, Godot.Collections.Array<DamageType> types, Godot.Collections.Array<DamageSource> sources)
    {
        PassiveUpgrade passiveUpgrade = new PassiveUpgrade();

        passiveUpgrade.AddUpgrades(GetPassiveUpgrade(spellName));

        passiveUpgrade.AddUpgrades(GetPassiveUpgrade(types));

        passiveUpgrade.AddUpgrades(GetPassiveUpgrade(sources));

        return passiveUpgrade;
    }

    public PassiveUpgrade GetPassiveUpgrade(string spellName)
    {
        PassiveUpgrade passiveUpgrade = new PassiveUpgrade();
        PassiveUpgrade value;

        if (passiveUpgradesBySpellName.TryGetValue(spellName, out value))
        {
            passiveUpgrade.AddUpgrades(value);
        }

        return passiveUpgrade;
    }

    public PassiveUpgrade GetPassiveUpgrade(Godot.Collections.Array<DamageType> types)
    {

        PassiveUpgrade passiveUpgrade = new PassiveUpgrade();
        PassiveUpgrade value;

        foreach (DamageType type in types)
        {
            if (passiveUpgradesByDamageType.TryGetValue(type, out value))
            {
                passiveUpgrade.AddUpgrades(value);
            }
        }

        return passiveUpgrade;
    }

    public PassiveUpgrade GetPassiveUpgrade(Godot.Collections.Array<DamageSource> sources)
    {
        PassiveUpgrade passiveUpgrade = new PassiveUpgrade();
        PassiveUpgrade value;

        foreach (DamageSource source in sources)
        {
            if (passiveUpgradesByDamageSource.TryGetValue(source, out value))
            {
                passiveUpgrade.AddUpgrades(value);
            }
        }

        return passiveUpgrade;
    }
}

