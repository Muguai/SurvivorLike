using Godot;
using System;

public partial class Pig : EnemyMovement, IEnemy
{
    public void OnSpawn(){
        GD.Print("Spawn");
    }



}
