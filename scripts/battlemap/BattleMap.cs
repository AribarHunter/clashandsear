using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class BattleMap : TileMap
{

    public int width;
    public int height;
    public BattleMapTile[,] tiles;

    public BattleMap(int width, int height, string name)
    {
        // Set up tile stuff.
        this.height = height;
        this.width = width;
        Name = name;
        tiles = new BattleMapTile[width, height];

        // Hardcoded TileSet for now.
        Set(PropertyName.TileSet, ResourceLoader.Load("resources/tilesets/testTileset.tres", PropertyName.TileSet));
    }

    public void FillMapWithTileSetTile(Vector2I tileSetAtlasCoords)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // BattleMapTile bmt = new BattleMapTile();
                SetCell(0, new Vector2I(x, y), 0, tileSetAtlasCoords);
                // GD.Print("We set a cell!");
            }
        }
    }

    public void FillMapWith2x2TileSetTile(Vector2I tileSetAtlasCoordsTopLeft, Vector2I tileSetAtlasCoordsBottomRight)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x % 2 == 0 && y % 2 == 0)
                    SetCell(0, new Vector2I(x, y), 0, new Vector2I(tileSetAtlasCoordsTopLeft.X, tileSetAtlasCoordsBottomRight.Y));
                else if (x % 2 == 1 && y % 2 == 0)
                    SetCell(0, new Vector2I(x, y), 0, new Vector2I(tileSetAtlasCoordsBottomRight.X, tileSetAtlasCoordsBottomRight.Y));
                else if (x % 2 == 1 && y % 2 == 1)
                    SetCell(0, new Vector2I(x, y), 0, new Vector2I(tileSetAtlasCoordsBottomRight.X, tileSetAtlasCoordsTopLeft.Y));
                else if (x % 2 == 0 && y % 2 == 1)
                    SetCell(0, new Vector2I(x, y), 0, new Vector2I(tileSetAtlasCoordsTopLeft.X, tileSetAtlasCoordsTopLeft.Y));
                else
                    SetCell(0, new Vector2I(x, y), 0, new Vector2I(0, 0));
                // GD.Print("We set a cell!");
            }
        }
    }

    public void FillMapWithCheckerboard(Vector2I firstCoord, Vector2I secondCoord)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if ((x + y) % 2 == 0)
                {
                    SetCell(0, new Vector2I(x, y), 0, firstCoord);
                }
                else
                {
                    SetCell(0, new Vector2I(x, y), 0, secondCoord);
                }
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