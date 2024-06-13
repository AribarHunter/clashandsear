using Godot;
using System;

public partial class Main : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Let's make a level.
		string name = "TestMap";
		BattleMapGenerator battleMapGenerator = new(this);
		BattleMap testMap = battleMapGenerator.CreateBattleMap(name);

		// Let's make a player and add them?
		Entity player = new Entity("Player", (Texture2D)ResourceLoader.Load("res://resources/animatedTextures/testCharTexture.tres"));
		battleMapGenerator.AddEntityToPosition(player, new Vector2I(2, 6));

		// Let's add a cursor?
		BattleMapCursor battleMapCursor = (BattleMapCursor)ResourceLoader.Load<PackedScene>("res://scenes/battlemapcursor.tscn").Instantiate();
		testMap.AddChild(battleMapCursor);

		// And here's the state machine stuff again.
		StateMachine stateMachine = new StateMachine(this);
		stateMachine.CurrentState = new PlayerTurnBaseState();

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void gonnaTestThis()
	{

	}
}
