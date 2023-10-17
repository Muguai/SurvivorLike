using Godot;
using System;

public partial class FpsCounter : Label
{

	public override void _Ready()
	{
		 EventManager.Instance.Connect("OnLevelUp", Callable.From(() => {
			GD.Print("Hello");
		}));
		
	}
	public override void _Process(double delta)
	{
		this.Text = "Fps: " + Engine.GetFramesPerSecond().ToString();
	}
}
