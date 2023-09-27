using Godot;
using System;

public partial class Empty : ISpell
{
    public string SpellName { get; set; } = "Empty";
    public Texture2D SpellLogo { get; set; } = null;

    public void Cast()
    {
        return;
    }

    public void Upgrade()
    {
        return;
    }

}
