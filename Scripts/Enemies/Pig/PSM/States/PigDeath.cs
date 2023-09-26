using Godot;
using System;
using System.Collections.Generic;

public partial class PigDeath : PigState
{
	public override void OnStart(Dictionary<string, object> message)
	{
		base.OnStart(message);
        PSM.anim.Play("Death");
		PSM._Pig.CollisionLayer = 3;
	}
	public override void UpdateState(double delta)
	{
		base.UpdateState(delta);
		if(!PSM.anim.IsPlaying()){
			PSM._Pig.QueueFree();
		}
	}
}