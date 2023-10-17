using Godot;
using System;
using SpellUtils;

public partial class Fireball : Node2D, ISpell
{
	[Export]
	public string SpellName { get; set; } = "Fireball";

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

	[Export]
	public const float SpawnOffest = 50.0f;
	[Export]
	public float CurveAmplitude { get; set; } = 5.0f;
	[Export]
	public float CurveFrequency { get; set; } = 1.0f;

	private bool collided = false;

	private Stats stat;

	public override void _Ready()
	{
		base._Ready();

		stat = GetNode<Stats>("Stats");
		InitializeSpell.Initialize(this, stat);
		GD.Print("Fireball stats " + stat.ToString());
		CollideBox = GetNode<Area2D>("CollideBox");
		ExplosionHitBox = GetNode<Area2D>("HitBox");
		Cast();
	}
	public void Cast()
	{
		CalculateStartPos();
		direction = (targetPosition - GlobalPosition).Normalized();
		FireballParticle.Emitting = true;
		FireballExplosionParticle.Emitting = false;
		isCast = true;
	}

	private void CalculateStartPos()
	{
		targetPosition = GetGlobalMousePosition();
		CharacterBody2D player = (CharacterBody2D)GetTree().GetFirstNodeInGroup("Player");
		Vector2 direction = (targetPosition - player.Position).Normalized();
		this.GlobalPosition = player.Position + (direction * SpawnOffest);
		Vector2 rightVector = direction.Rotated(Mathf.Pi / 2);
		Vector2 leftVector = -rightVector;

		Random r = new Random();
		int leftRight = r.Next(0, 2);
		if (leftRight == 0)
		{
			curveVector = leftVector;
		}
		else
		{
			curveVector = rightVector;
		}

		CurveAmplitude = (float)r.Next(0, 5);

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

		if (!isCast)
		{
			return;
		}


		if (!FireballParticle.Emitting && !FireballExplosionParticle.Emitting)
		{
			QueueFree();
		}

		if (FireballExplosionParticle.Emitting)
		{
			return;
		}


		if (collided)
		{
			CollideBox.Monitoring = false;
			ExplosionHitBox.Monitoring = true;
			FireballParticle.Emitting = false;
			FireballExplosionParticle.Emitting = true;
			return;
		}


		GlobalPosition += direction * stat.Speed * (float)delta;
		destoryTimer += (float)delta;
		if (time < 1)
		{
			time += (float)delta;
		}
		else
		{
			time -= (float)delta;
		}
		float offset = Mathf.Sin(time * CurveFrequency) * CurveAmplitude;

		GlobalPosition += curveVector * offset;

		if (destoryTimer > 20f)
		{
			QueueFree();
		}


	}

	public void _on_collide_box_body_entered(Node Body)
	{
		if (Body.IsInGroup("Enemies"))
		{
			collided = true;
		}
	}

}
