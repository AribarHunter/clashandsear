using Godot;
using System;

public partial class Main : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print($"Main's script is... {GetScript()}");

		// Let's make a level.
		string name = "TestMap";
        BattleMapGenerator battleMapGenerator = new(this);
        battleMapGenerator.CreateBattleMap(name);

        GetNode<BattleMap>(name).test();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void gonnaTestThis() {
		
	}
}
