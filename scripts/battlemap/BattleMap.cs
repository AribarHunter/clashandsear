using Godot;
using System;

public partial class BattleMap : TileMap
{

    public int width;
    public int height;
    public BattleMapTile[,] tiles;

    public BattleMap(int width, int height) {
        this.height = height;
        this.width = width;

        tiles = new BattleMapTile[width, height];

        // Hardcoded TileSet for now.
        // this.Set(PropertyName.TileSet, )

    }

	// Called when the node enters the scene tree for the first time.
	// public override void _Ready()
	// {
	// }

	// // Called every frame. 'delta' is the elapsed time since the previous frame.
	// public override void _Process(double delta)
	// {
	// }

    public void test() {
        GD.Print($"Yay!");
    }
}