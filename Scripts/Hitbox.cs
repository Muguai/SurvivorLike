using Godot;
using System;
using System.Linq;

public partial class Hitbox : Area2D
{

	[Export]
	public int damageAmount {get; set;} = 1;

	public void _on_body_entered(Node2D body){
		GD.Print("Body");
		Health hp = body.GetChildren().FirstOrDefault(child => child is Health) as Health;
		if(hp != null)
			hp.Damage(damageAmount);
	}
}
