using ClashAndSear.scripts.entity;
using Godot;

namespace ClashAndSear.scripts;

public partial class Main : Node2D
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // Let's make a signal manager.
        SignalManager signalManager = new(this);

        // Let's make something to hold game context.
        GameContext gameContext = new(this);

        // Let's make a level.
        string name = "TestMap";
        BattleMapGenerator battleMapGenerator = new(this);
        BattleMap testMap = battleMapGenerator.CreateBattleMap(name);

        // Let's make a player and add them?
         var test = GD.Load<PackedScene>("res://scenes/entities/actor.tscn");
        Actor player = test.Instantiate<Actor>();
        battleMapGenerator.AddEntityToPosition(player, new Vector2I(2, 6), testMap.tiles[2, 6]);
        
        Actor someOtherDood = test.Instantiate<Actor>();
        battleMapGenerator.AddEntityToPosition(someOtherDood, new Vector2I(3, 6), testMap.tiles[3, 6]);
        
        Actor aThirdGal = test.Instantiate<Actor>();
        battleMapGenerator.AddEntityToPosition(aThirdGal, new Vector2I(4, 4), testMap.tiles[4, 4]);

        // 's add a cursor?
        BattleMapCursor battleMapCursor = (BattleMapCursor)ResourceLoader.Load<PackedScene>("res://scenes/battlemapcursor.tscn").Instantiate();
        testMap.AddChild(battleMapCursor);

        // And here's the state machine stuff again.
        StateMachine stateMachine = new(this, signalManager);
        stateMachine.CurrentState = new PlayerTurnBaseState();

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}