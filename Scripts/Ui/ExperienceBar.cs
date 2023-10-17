using Godot;
using System;

public partial class ExperienceBar : HSlider
{
	
	private RichTextLabel levelText;
	private int level = 1;
	public override void _Ready()
	{
		levelText = GetParent().GetNode<RichTextLabel>("Level");
		levelText.Text =  "[center]" + level.ToString();
		EventManager.Instance.Connect("OnExperiencePickup", Callable.From(() => {
			AddExp(5);
		}));
	}

	float timer = 0;
	public override void _Process(double delta)
	{
		timer += (float)delta;
		if(timer > 0.5){
			AddExp(11);
			timer = 0;
		}

	}

	public void AddExp(double amount){
		if(Value + amount >= MaxValue){
			double tempVal = Value;
			Value = Value + amount - MaxValue; 
			level += 1;
			levelText.Text = "[center]" + level.ToString();
			AddExp(tempVal + amount - MaxValue);
			EventManager.Instance.EmitSignal(EventManager.SignalName.OnLevelUp);

		}else{
			Value += amount;
		}

	}
}
