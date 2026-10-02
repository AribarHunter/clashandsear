using Godot;
using ClashAndSear.scripts.utility;
using ClashAndSear.scripts.battlemap;
using ClashAndSear.scripts.entity;

namespace ClashAndSear.scripts
{
    
    public partial class Main : Node2D
    {
        private SignalManager _signalManager;
        private GameContext _gameContext;
    
        public override void _Ready()
        {
            _signalManager = GetNode<SignalManager>("%SignalManager");
            _gameContext = GetNode<GameContext>("%GameContext");

            // Let's make a level.
            BattleMapGenerator battleMapGenerator = new(this);
            _gameContext.battleMap = battleMapGenerator.CreateBattleMap("TestMap");

            // Let's make a player and add them?
            PackedScene test = GD.Load<PackedScene>("res://scenes/entities/actor.tscn");
            Actor player = test.Instantiate<Actor>();
            battleMapGenerator.AddEntityToBattleMapAtPosition(player, _gameContext.battleMap, new Vector2I(2, 6));
        
            Actor someOtherDood = test.Instantiate<Actor>();
            battleMapGenerator.AddEntityToBattleMapAtPosition(someOtherDood, _gameContext.battleMap, new Vector2I(3, 6));
        
            Actor aThirdGal = test.Instantiate<Actor>();
            battleMapGenerator.AddEntityToBattleMapAtPosition(aThirdGal, _gameContext.battleMap, new Vector2I(4, 4));

        
            GameContext.Instance.stateMachine.CurrentState = new statemachine.states.BaseTurnState();
        }
    }
}