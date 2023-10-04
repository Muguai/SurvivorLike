using Godot;
using System;

public partial class EventManager : Node
{
    private static EventManager _instance;
    public static EventManager Instance => _instance;

    // Signals
    [Signal] public delegate void OnLevelUpEventHandler();
    [Signal] public delegate void OnExperiencePickupEventHandler(double amount);


    public override void _Ready()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            QueueFree();
        }
    }
}
