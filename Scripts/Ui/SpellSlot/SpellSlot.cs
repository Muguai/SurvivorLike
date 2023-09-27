using Godot;

public partial class SpellSlot : Control
{
    private TextureRect spellLogo;
    private RichTextLabel spellNameLabel;
    private RichTextLabel slotNumberLabel;
    public int slotNumber {get; set;} = 0;
    public ISpell Spell {get; set;} = null;
    
    public override void _Ready()
    {
        spellLogo = GetNode<TextureRect>("SpellLogo");
        spellNameLabel = GetNode<RichTextLabel>("SpellNameLabel");
        slotNumberLabel = GetNode<RichTextLabel>("SlotNumberLabel");
        slotNumberLabel.Text = "[center]"+slotNumber.ToString();
    }

    public void UpdateSpellSlot(ISpell spell)
    {
        Spell = spell;
        spellNameLabel.Text = "[center]"+spell.SpellName;
        spellLogo.Texture = spell.SpellLogo;
    }
}