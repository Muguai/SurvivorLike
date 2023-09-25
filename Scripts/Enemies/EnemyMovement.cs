using Godot;
using System;

public partial class EnemyMovement : CharacterBody2D
{
   	[Export]
	AnimationPlayer animationPlayer;
	[Export]
	Sprite2D enemySprite;
	public const float Speed = 100.0f;    
	public PlayerMovement player {get; set;}


	public override void _Process(double delta){
		Vector2 velocity = Velocity;


		Vector2 direction = this.Position.DirectionTo( player.Position);

		if(direction != Vector2.Zero){
			animationPlayer.Play("Run");
		}

		if (direction.X != 0)
		{
			velocity.X = direction.X * Speed;
			if(direction.X > 0){
				enemySprite.FlipH = true;
			}else{
				enemySprite.FlipH = false;
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
