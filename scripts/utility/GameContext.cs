using Godot;
using System;


public partial class GameContext : Node
{
    public static GameContext Instance { get; private set; }

    public ClashAndSear.scripts.entity.Actor selectedActor;
    public ClashAndSear.scripts.statemachine.State currentState;

    public GameContext(Node2D parentNode)
    {
        Name = "GameContext";
        parentNode.AddChild(this);
    }

    public override void _Ready()
    {
        Instance = this;
    }
}
