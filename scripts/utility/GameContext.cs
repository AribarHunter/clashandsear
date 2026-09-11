using ClashAndSear.scripts.statemachine;
using Godot;

namespace ClashAndSear
{
    public partial class GameContext : Node
    {
        public static GameContext Instance { get; private set; }

        public Actor selectedActor;
    
        // TODO change to use this instead of currentState?
        [Export] public StateMachine stateMachine;

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
}