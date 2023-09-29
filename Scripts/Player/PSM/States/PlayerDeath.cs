using Godot;
using System;
using System.Collections.Generic;

public partial class PlayerDeath : PlayerState
{
 
	public override void OnStart(Dictionary<string, object> message)
	{
		base.OnStart(message);
        PSM.anim.Play("Idle");
	}

	public override void UpdateState(double delta)
	{
		base.UpdateState(delta);

		
	}
}