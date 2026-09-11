using ClashAndSear.scripts.entity;
using ClashAndSear.scripts.utility;
using Godot;

namespace ClashAndSear.scripts;

public partial class Main : Node2D
{
    private SignalManager _signalManager;
    private GameContext _gameContext;
    
    public override void _Ready()
    {
        _signalManager = GetNode<SignalManager>("%SignalManager");
        _gameContext = GetNode<GameContext>("%GameContext");

        // Let's make a level.
        battlemap.BattleMapGenerator battleMapGenerator = new(this);
        battlemap.BattleMap testMap = battleMapGenerator.CreateBattleMap("TestMap");

        // Let's make a player and add them?
        PackedScene test = GD.Load<PackedScene>("res://scenes/entities/actor.tscn");
        Actor player = test.Instantiate<Actor>();
        battleMapGenerator.AddEntityToPosition(player, new Vector2I(2, 6), testMap.tiles[2, 6]);
        
        Actor someOtherDood = test.Instantiate<Actor>();
        battleMapGenerator.AddEntityToPosition(someOtherDood, new Vector2I(3, 6), testMap.tiles[3, 6]);
        
        Actor aThirdGal = test.Instantiate<Actor>();
        battleMapGenerator.AddEntityToPosition(aThirdGal, new Vector2I(4, 4), testMap.tiles[4, 4]);

        // 's add a cursor?
        ui.BattleMapCursor battleMapCursor = (ui.BattleMapCursor)ResourceLoader.Load<PackedScene>("res://scenes/battlemapcursor.tscn").Instantiate();
        testMap.AddChild(battleMapCursor);
        
        utility.GameContext.Instance.stateMachine.CurrentState = new statemachine.states.BaseTurnState();
    }
}