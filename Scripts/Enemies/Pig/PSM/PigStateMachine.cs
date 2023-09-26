using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

public partial class PigStateMachine : StateMachine
{
	[Export]
	public Pig _Pig;
	[Export]
	public AnimationPlayer anim;
	[Export]
	public AudioStreamPlayer audioSteamPlayer;
	public override void _Ready()
	{
		base._Ready();

		List<PigState> pigManageStates = GetNode<Node>("States").GetChildren().OfType<PigState>().ToList();

		foreach(PigState PS in pigManageStates)
		{
			PS.PSM = this;
		}

		ChangeState(pigManageStates[0].Name);
	}
	
	
}