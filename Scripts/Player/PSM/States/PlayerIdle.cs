using Godot;
using System;
using System.Collections.Generic;

public partial class PlayerIdle : PlayerState
{
 
	public override void OnStart(Dictionary<string, object> message)
	{
		base.OnStart(message);
		PSM.anim.Play("Idle");
		GD.Print("PlayerIdle");
	}

	public override void UpdateState(double delta)
	{
		base.UpdateState(delta);
		
		Vector2 direction = Input.GetVector("Left", "Right", "Up", "Down");
		if(direction.Y != 0 || direction.X != 0){
			PSM.ChangeState("PlayerRun");
		}

		
	}
}
