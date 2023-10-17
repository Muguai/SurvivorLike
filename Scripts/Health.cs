using Godot;
using System;
using System.Linq;

public partial class Health : Node
{
    [Export]
    public float Hp = 1;
    
    [Export]
    public String DeathState = null;

    [Export]
    public Sprite2D flickerSprite = null;

    [Export]
    public double InvincibilityFrames = 0;
    private double InvincibilityTimer = 0;
    private bool Invincible = true;

    
    [Export]
    public bool ShowDamageNumber = true;


    public override void _Ready()
    {
        InvincibilityTimer = InvincibilityFrames;
        
    }

    public override void _Process(double delta)
    {
        InvincibilityTimer += delta;

        if (InvincibilityTimer < InvincibilityFrames)
        {
            float opacityValue = (float)Math.Abs(Math.Sin(InvincibilityTimer * 10)); 
            flickerSprite.Modulate = new Color(1, 1, 1, opacityValue);
        }
        
    }

    public void Damage(Damage damage)
    {
        if (InvincibilityTimer <= InvincibilityFrames)
        {
            return;
        }

        Hp -= damage.Amount;
        InvincibilityTimer = 0;
        if (Hp < 1)
        {
            StateMachine sm = GetParent().GetNode<StateMachine>("StateMachine");
            sm.ChangeState(DeathState);
        }

        if(ShowDamageNumber){
            RichTextLabel damageLabel = new RichTextLabel();
            damageLabel.Text = damage.Amount.ToString();
            damageLabel.Size = new Vector2(200,200);
            damageLabel.Position = new Vector2(0, -30); 
            damageLabel.Modulate = new Color(1, 1, 1, 1);
            damageLabel.ZIndex = 100;
            damageLabel.AddThemeConstantOverride("outline_size", 5); 
            damageLabel.AddThemeColorOverride("font_outline_color", new Color (0,0,0, 1));


            GetParent().AddChild(damageLabel);

            var tween = GetTree().CreateTween().BindNode(this).SetTrans(Tween.TransitionType.Expo);
            tween.TweenProperty(damageLabel, "position", new Vector2(0, -45), 0.5f);
            tween.TweenProperty(damageLabel, "modulate", new Color(1, 1, 1, 0), 0.5f);
            tween.TweenCallback(Callable.From(damageLabel.QueueFree));
        }
  
    }
}
