using Godot;

public partial class Item : Node2D
{
    [Export]
    public string ItemName { get; set; }
    [Export]
    public Texture2D Icon { get; set; }
}
