using Godot;

namespace ClashAndSear
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
            battleMapGenerator.AddEntityToPosition(player, new Vector2I(2, 6), _gameContext.battleMap.tiles[2, 6]);
        
            Actor someOtherDood = test.Instantiate<Actor>();
            battleMapGenerator.AddEntityToPosition(someOtherDood, new Vector2I(3, 6), _gameContext.battleMap.tiles[3, 6]);
        
            Actor aThirdGal = test.Instantiate<Actor>();
            battleMapGenerator.AddEntityToPosition(aThirdGal, new Vector2I(4, 4), _gameContext.battleMap.tiles[4, 4]);

        
            GameContext.Instance.stateMachine.CurrentState = new BaseTurnState();
        }
    }
}