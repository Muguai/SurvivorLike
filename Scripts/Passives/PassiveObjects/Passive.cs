using Godot;
using System;
using System.Collections.Generic;

public partial class Passive : Resource{

    public Godot.Collections.Array<PassiveUpgrade> passiveUpgrades {get; set;} = new Godot.Collections.Array<PassiveUpgrade>();

}