using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

public partial class PlayerStateMachine : StateMachine
{
	[Export]
	public CharacterBody2D _Player;
	[Export]
	public AnimationPlayer anim;
	
	[Export]
	public AudioStreamPlayer audioSteamPlayer;
	public override void _Ready()
	{
		base._Ready();

		List<PlayerState> playerManageStates = GetNode<Node>("States").GetChildren().OfType<PlayerState>().ToList();

		foreach(PlayerState PS in playerManageStates)
		{
			PS.PSM = this;
		}

		ChangeState(playerManageStates[0].Name);
	}
	
	
}