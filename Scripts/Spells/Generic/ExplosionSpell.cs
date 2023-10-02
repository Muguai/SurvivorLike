using Godot;
using System;

public partial class ExplosionSpell : Node2D
{
    public int ExplosionRadiusMultiplier {get; set;} = 1;

	public void InitializeExplosion(Node node){
        // Set ExplosionRadiusMultiplier From Passive Singleton

        //Get All Children within Node that is in Explosion Group

        //Loop Through them
            //If Particle increase Amount by Mulyiplier
            //If Area2D Increase Scale by multipler

    }
}
