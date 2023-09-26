using Godot;

public partial class SpellSlot : Control
{
    private TextureRect spellLogo;
    private RichTextLabel spellNameLabel;
    private RichTextLabel slotNumberLabel;
    public int slotNumber {get; set;} = 0;
    
    public override void _Ready()
    {
        spellLogo = GetNode<TextureRect>("SpellLogo");
        spellNameLabel = GetNode<RichTextLabel>("SpellNameLabel");
        slotNumberLabel = GetNode<RichTextLabel>("SlotNumberLabel");
        slotNumberLabel.Text = "[center]"+slotNumber.ToString();
    }

    public void UpdateSpellSlot(string spellName, Texture2D texture)
    {
        spellNameLabel.Text = "[center]"+spellName;
        spellLogo.Texture = texture;
    }
}