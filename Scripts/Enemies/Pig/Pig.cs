using Godot;
using System;

public partial class Pig : Enemy, IEnemy
{
    public void OnSpawn(){
        GD.Print("Spawn");
    }
}
