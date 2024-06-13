using Godot;
using System;

public partial class BattleMapCursor : Node2D
{
	BattleMap battleMap;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// GD.Print("In BattleMapCursor._Ready()");
		battleMap = GetParent<BattleMap>();
		Set(PropertyName.Position, battleMap.MapToLocal(Vector2I.Zero));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
