using Godot;

namespace ClashAndSear.scripts.utility;

public partial class GameContext : Node
{
    public static GameContext Instance { get; private set; }

    public entity.Actor selectedActor;
    public statemachine.State currentState;

    public GameContext(Node2D parentNode)
    {
        Name = "GameContext";
        parentNode.AddChild(this);
    }

    public GameContext()
    {
    }

    public override void _Ready()
    {
        Instance = this;
    }
}