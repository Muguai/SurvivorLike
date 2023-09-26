using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Spawner : Node
{
	public List<Node2D> spawnPoints;
	[Export] public PlayerMovement player;

	private float minDistance = 500f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var points = GetChildren().Where(child => child is Node2D).Select(child => child).Cast<Node2D>(); 

		spawnPoints = new List<Node2D>();

		foreach(var point in points)
		{
	   		spawnPoints.Add(point);
		}
	}


	public Vector2 getRandomSpawnPos(){
		Random r = new Random();
		int X = r.Next((int)spawnPoints[2].Position.X,(int)spawnPoints[3].Position.X );
		int Y = r.Next((int)spawnPoints[0].Position.Y,(int)spawnPoints[1].Position.Y );

		Vector2 spawnPos = new (X, Y);
		
		GD.Print(spawnPos);

		return spawnPos;
	}

	double timer = 0;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		timer += delta;

		if(timer > 1){
			SpawnEnemy("Pig");
			timer = 0;
		}

	}


	public void SpawnEnemy(string enemyName){
		Vector2 vec;

		while(true){
			vec = getRandomSpawnPos();
			if(player.Position.DistanceSquaredTo(vec) > (minDistance * minDistance))
				break;
		}

		string path = "res://Prefabs/Enemies/" + enemyName + ".tscn";
		var packedScene = GD.Load<PackedScene>(path);
		

		EnemyMovement e = (EnemyMovement)packedScene.Instantiate();
		e.Position = vec;
		e.player = player;
		IEnemy ie = (IEnemy)e;
		ie.OnSpawn();
		CallDeferred("add_child", e);	
	}
}
