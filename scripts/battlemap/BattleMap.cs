using Godot;
using System;

public partial class BattleMap : TileMap
{

    public int width;
    public int height;
    public BattleMapTile[,] tiles;

    public BattleMap(int width, int height) {
        // Set up tile stuff.
        this.height = height;
        this.width = width;
        tiles = new BattleMapTile[width, height];

        // Hardcoded TileSet for now.
        Set(PropertyName.TileSet, ResourceLoader.Load("resources/tilesets/testTileset.tres", PropertyName.TileSet));
    }

    public void FillMapWithTileSetTile(Vector2I tileSetAtlasCoords) {
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                // BattleMapTile bmt = new BattleMapTile();
                SetCell(0, new Vector2I(x, y), 0, tileSetAtlasCoords);
                // GD.Print("We set a cell!");
            }
        }
    }

	// Called when the node enters the scene tree for the first time.
	// public override void _Ready()
	// {
	// }

	// // Called every frame. 'delta' is the elapsed time since the previous frame.
	// public override void _Process(double delta)
	// {
	// }
}