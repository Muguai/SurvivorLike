using Godot;
using System;

public partial class PlayerMovement : CharacterBody2D
{
	[Export]
	AnimationPlayer animationPlayer;

	
	[Export]
	Sprite2D playerSprite;
	public const float Speed = 300.0f;


	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");

		if(direction != Vector2.Zero){
			animationPlayer.Play("Run");
		}else{
			animationPlayer.Play("Idle");
		}

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
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}


		
		if(direction.Y != 0)
		{
			velocity.Y = direction.Y * Speed;
		}else
		{
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
