using Godot;
using System;
using System.Collections.Generic;

public partial class SlotHolder : HBoxContainer
{
	private int selectedSlot = 0;
	private int maxSlots = 4;

	private ISpell activeSpell;

	public override void _Ready()
	{
		CreateSpellSlots(maxSlots);
		SelectSpellSlot(0);
		Fireball f = new Fireball();
		AddSpellToSelectedSlot(f);
	}


	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseKeyEvent && mouseKeyEvent.Pressed && mouseKeyEvent.ButtonIndex == MouseButton.Left && activeSpell.SpellName != "Empty")
		{
			var spellSlotInstance = ResourceLoader.Load<PackedScene>("res://Prefabs/Spells/" + activeSpell.SpellName + ".tscn").Instantiate();
			GetTree().Root.AddChild(spellSlotInstance);

		}


		if (@event is InputEventKey keyEvent && keyEvent.Pressed)
		{
			if ((int)keyEvent.Keycode >= (int)Key.Key1 &&
				(int)keyEvent.Keycode <= (int)Key.Key9)
			{
				int slotIndex = (int)keyEvent.Keycode - (int)Key.Key1;
				if (slotIndex < maxSlots)
				{
					SelectSpellSlot(slotIndex);
				}
			}
			else if (keyEvent.Keycode == Key.Q)
			{
				SelectPreviousSlot();
			}
			else if (keyEvent.Keycode == Key.E)
			{
				SelectNextSlot();
			}
		}
	}
	private void SelectSpellSlot(int slotIndex)
	{
		var selectedSpellSlot = this.GetChild<SpellSlot>(selectedSlot);
		selectedSpellSlot.Modulate = new Color(1, 1, 1);

		selectedSlot = slotIndex;
		var newSelectedSpellSlot = this.GetChild<SpellSlot>(selectedSlot);
		newSelectedSpellSlot.Modulate = new Color(0.8f, 0.8f, 0.8f);

		activeSpell = newSelectedSpellSlot.Spell;
	}

	private void SelectPreviousSlot()
	{
		if (selectedSlot > 0)
		{
			SelectSpellSlot(selectedSlot - 1);
		}
		else
		{
			SelectSpellSlot(this.GetChildCount() - 1);
		}
	}

	private void SelectNextSlot()
	{
		if (selectedSlot < this.GetChildCount() - 1)
		{
			SelectSpellSlot(selectedSlot + 1);
		}
		else
		{
			SelectSpellSlot(0);
		}
	}

	public void AddSpellToSelectedSlot(ISpell spell)
	{
		var selectedSpellSlot = this.GetChild<SpellSlot>(selectedSlot);
		selectedSpellSlot.UpdateSpellSlot(spell);

		activeSpell = selectedSpellSlot.Spell;
	}

	private void CreateSpellSlots(int numSlots)
	{
		for (int i = 0; i < numSlots; i++)
		{
			var spellSlotInstance = ResourceLoader.Load<PackedScene>("res://Prefabs/Ui/SpellSlots/Slot.tscn").Instantiate();
			SpellSlot s = (SpellSlot)spellSlotInstance;
			s.slotNumber = i + 1;
			this.AddChild(s);
			Empty e = new Empty();
			s.UpdateSpellSlot(e);
		}
	}
}
