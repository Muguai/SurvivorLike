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
	}

	float timer = 0;
	public override void _Process(double delta)
	{
		timer += (float)delta;
		if(timer > 1){
			AddExp(3);
			timer = 0;
		}

	}

	public void AddExp(int amount){
		if(Value + amount >= MaxValue){
			Value = Value + amount - MaxValue; 
			level += 1;
			levelText.Text = "[center]" + level.ToString();
		}else{
			Value += amount;
		}

	}
}
