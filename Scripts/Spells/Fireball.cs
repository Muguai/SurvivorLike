using Godot;
using System;

public partial class Fireball : Node2D, ISpell
{
	[Export]
	public string SpellName {get; set;} = "Fireball";
	
	[Export]
	public Texture2D SpellLogo { get; set; } = null;

	
	[Export]
	public GpuParticles2D FireballParticle { get; set; }

	[Export]
	public GpuParticles2D FireballExplosionParticle { get; set; }

	
	[Export]
	public Area2D CollideBox { get; set; }

	
	[Export]
	public Area2D ExplosionHitBox { get; set; }

	private bool isCast = false;
	private Vector2 targetPosition = Vector2.Zero; 
	private Vector2 direction = Vector2.Down;
	
	private Vector2 curveVector = Vector2.Down;
	private float halfDistance = 0f;
	[Export]
	public const float Speed = 300.0f;

	[Export]
	public const float SpawnOffest = 50.0f;
	[Export]
	public float CurveAmplitude { get; set; } = 5.0f; // Adjust this value to control the curve's amplitude
	[Export]
	public float CurveFrequency { get; set; } = 1.0f;  // Adjust this value to control the curve's frequency

	private bool collided = false;

	public override void _Ready()
	{
		FireballParticle = GetNode<GpuParticles2D>("FireballParticle");
		FireballExplosionParticle = GetNode<GpuParticles2D>("FireballExplosion");
		CollideBox = GetNode<Area2D>("CollideBox");
		ExplosionHitBox = GetNode<Area2D>("HitBox");
		Cast();
	}
	public void Cast()
	{
		GD.Print("------Start---------");
		CalculateStartPos();
		direction = (targetPosition - GlobalPosition).Normalized();
		FireballParticle.Emitting = true;
		FireballExplosionParticle.Emitting = false;
		isCast = true;
	}

	private void CalculateStartPos(){
		targetPosition = GetGlobalMousePosition();
		CharacterBody2D player = (CharacterBody2D)GetTree().GetFirstNodeInGroup("Player");
		Vector2 direction = (targetPosition - player.Position).Normalized();
		this.GlobalPosition = player.Position + (direction * SpawnOffest);
		GD.Print(this.Position);
		halfDistance = GlobalPosition.DistanceTo(targetPosition) / 2;
		Vector2 rightVector = direction.Rotated(Mathf.Pi / 2);
		Vector2 leftVector = -rightVector;

		Random r = new Random();
		int leftRight = r.Next(0,2);
		GD.Print(leftRight);
		if(leftRight == 0){
			curveVector = leftVector;
		}else{
			curveVector = rightVector;
		}

		CurveAmplitude = (float)r.Next(0,10);

	
	}

	public void Upgrade()
	{
		throw new NotImplementedException();
	}

	float time = 0;
	float destoryTimer = 0;
	public override void _Process(double delta)
	{
		base._Process(delta);

		if(!isCast){
			return;
		}
			  

		if(!FireballParticle.Emitting && !FireballExplosionParticle.Emitting){
			QueueFree();
		}

		if(FireballExplosionParticle.Emitting){
			return;
		}

		
		if (collided)
		{
			GD.Print("------End---------");
			CollideBox.Monitoring = false;
			ExplosionHitBox.Monitoring = true;
			FireballParticle.Emitting = false;
			FireballExplosionParticle.Emitting = true;
			return;
		}


        GlobalPosition += direction * Speed * (float)delta;
		destoryTimer += (float)delta;
		if(time < 1){
			time += (float)delta;
		}else{
			time -= (float)delta;
		}
   		 float offset = Mathf.Sin(time * CurveFrequency) * CurveAmplitude;

		GlobalPosition += curveVector * offset;

		if(destoryTimer > 20f){
			QueueFree();
		}

		
	}

	public void _on_collide_box_body_entered(Node Body){
		if(Body.IsInGroup("Enemies")){
			collided = true;
		}
	}

}
