using Godot;
using System;

public partial class Experience : Item, IItem
{
    public void OnSpawn(){

    }

    public void OnPickup(){
        EventManager.Instance.EmitSignal(EventManager.SignalName.OnExperiencePickup, 1.0);
    }
}
