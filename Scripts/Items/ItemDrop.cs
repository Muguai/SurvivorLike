using Godot;
using System;
using System.Collections.Generic;

public partial class ItemDrop : Node
{
    [Export]
    private Godot.Collections.Dictionary<string, float> itemsToDrop;

    public override void _Ready()
    {

    }

    public void DropItems(Vector2 dropPos)
    {
        Random r = new Random();
        foreach (KeyValuePair<string, float> item in itemsToDrop)
        {
            float val = (float)r.NextDouble();
            if(val <= item.Value)
                SpawnItem(item.Key, dropPos);
        }
    }

    public void SpawnItem(string itemName, Vector2 spawnPos)
    {

        string path = "res://Prefabs/Items/" + itemName + ".tscn";
        var packedScene = GD.Load<PackedScene>(path);

        Item i = (Item)packedScene.Instantiate();
        i.Position = spawnPos;
        IItem ii = (IItem)i;
        CallDeferred("add_child", i);
        ii.OnSpawn();
    }
}
