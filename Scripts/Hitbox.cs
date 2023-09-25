using Godot;
using System;
using System.Linq;

public partial class Hitbox : Area2D
{

	[Export]
	public int damageAmount {get; set;} = 1;

	public void _on_body_entered(Node2D body){
		GD.Print("Damage " + body.Name);
		GD.Print( body.GetChildren().ToString());
		Health hp = body.GetChildren().FirstOrDefault(child => child is Health) as Health;
		hp.Damage(damageAmount);
	}
}
