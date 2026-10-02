using Godot;

namespace ClashAndSear.scripts.utility
{
    public partial class GameContext : Node
    {
        public static GameContext Instance { get; private set; }

        public entity.Actor selectedActor;
        public Vector2I selectedPosition;
        public battlemap.BattleMap battleMap;
    
        [Export] public statemachine.StateMachine stateMachine;

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