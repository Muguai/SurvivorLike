using Godot;
using System;
using System.Collections.Generic;

public partial class PigRun : PigState
{
	[Export]
	Sprite2D playerSprite;
    [Export]
	public const float Speed = 100.0f;

	public override void OnStart(Dictionary<string, object> message)
	{
		base.OnStart(message);
        PSM.anim.Play("Run");

	}

	public override void UpdateState(double delta)
	{
		base.UpdateState(delta);
        PSM._Pig.MoveEnemyTowardsPlayer(Speed);
	}
}