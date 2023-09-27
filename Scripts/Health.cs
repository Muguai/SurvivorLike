using Godot;
using System;
using System.Linq;

public partial class Health : Node
{
    [Export]
    public int Hp = 1;

    
    [Export]
    public String DeathState = null;

    [Export]
    public Sprite2D flickerSprite = null;

    [Export]
    public double InvincibilityFrames = 0;
    private double InvincibilityTimer = 0;
    private bool Invincible = true;

    private Tween opacityTween;

    public override void _Ready()
    {
        /*
        opacityTween = new Tween();
        AddChild(opacityTween);

        opacityTween.Connect("tween_completed", this, "_on_TweenCompleted");
        */
    }

    public override void _Process(double delta)
    {
        InvincibilityTimer += delta;

        /*

        if (InvincibilityTimer < InvincibilityFrames)
        {
            float opacityValue = (float)Math.Abs(Math.Sin(InvincibilityTimer * 10)); 
            flickerSprite.Modulate = new Color(1, 1, 1, opacityValue);

            if (!opacityTween.IsRunning())
            {
                opacityTween.TweenInterval(flickerSprite, "modulate:a", 0f, 1f, 0.5f,
                    Tween.TransitionType.Sine, Tween.EaseType.InOut);
                opacityTween.Start();
            }
        }
        else
        {
            flickerSprite.Modulate = new Color(1, 1, 1, 1);
            opacityTween.Stop(flickerSprite);
        }
        */
    }

    public void Damage(int amount)
    {
        if (InvincibilityTimer <= InvincibilityFrames)
        {
            return;
        }
        Hp -= amount;
        InvincibilityTimer = 0;
        if (Hp < 1)
        {
            StateMachine sm = GetParent().GetNode<StateMachine>("StateMachine");
            sm.ChangeState(DeathState);
        }
    }

    private void _on_TweenCompleted(Object obj, string key)
    {
        if (key == "modulate:a")
        {
            flickerSprite.Modulate = new Color(1, 1, 1, 0);
        }
    }
}
