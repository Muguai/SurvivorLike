using Godot;
using System;
using System.Collections.Generic;

public partial class PlayerRun : PlayerState
{
	[Export]
	public Sprite2D playerSprite;
	[Export]
	public const float Speed = 300.0f;

	public override void OnStart(Dictionary<string, object> message)
	{
		base.OnStart(message);
		PSM.anim.Play("Run");
	}

	public override void UpdateState(double delta)
	{
		base.UpdateState(delta);
		
		Vector2 velocity = PSM._Player.Velocity;

		Vector2 direction = Input.GetVector("Left", "Right", "Up", "Down");

		if (direction.X != 0)
		{
			velocity.X = direction.X * Speed;
			if(direction.X > 0){
				playerSprite.FlipH = true;
			}else{
				playerSprite.FlipH = false;
			}
		}
		else
		{
			velocity.X = Mathf.MoveToward(PSM._Player.Velocity.X, 0, Speed);
		}
		
		if(direction.Y != 0)
		{
			velocity.Y = direction.Y * Speed;
		}else
		{
			velocity.Y = Mathf.MoveToward(PSM._Player.Velocity.Y, 0, Speed);
		}


		if(PSM._Player.Velocity == Vector2.Zero){
			PSM.ChangeState("PlayerIdle");
		}

		PSM._Player.Velocity = velocity;
		PSM._Player.MoveAndSlide();
	}
}
