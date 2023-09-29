using Godot;
using System;
using System.Linq;

public partial class Hitbox : Area2D
{
	[Export]
	public int damageAmount {get; set;} = 1;
	private Stats stat;

	public override void _Ready()
	{
		base._Ready();
		stat = GetParent().GetNode<Node>("Stats") as Stats;
	}

	public void _on_body_entered(Node2D body){
		Health hp = body.GetChildren().FirstOrDefault(child => child is Health) as Health;
		if(hp != null){
			Damage dmg = new Damage(stat);
			hp.Damage(dmg);
		}
	}
}
