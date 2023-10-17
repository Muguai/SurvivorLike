using Godot;
using System;

public interface ISpell
{
    [Export]
    public String SpellName {get; set;}
    [Export]
    public Texture2D SpellLogo{get; set;}

    public void Cast();

    public void Upgrade();
}
