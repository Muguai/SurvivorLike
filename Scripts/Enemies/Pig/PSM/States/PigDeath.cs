using Godot;
using System;
using System.Collections.Generic;

public partial class PigDeath : PigState
{
	public override void OnStart(Dictionary<string, object> message)
	{
		base.OnStart(message);
        PSM.anim.Play("Death");
	}
	public override void UpdateState(double delta)
	{
		base.UpdateState(delta);
		if(!PSM.anim.IsPlaying()){
			ItemDrop itemDrop = PSM._Pig.GetNode<ItemDrop>("ItemDrop");
			if(itemDrop != null)
            	itemDrop.DropItems(PSM._Pig.Position);
 
			PSM._Pig.QueueFree();
		}
	}
}