using Godot;
using System.Collections.Generic;

public partial class BattleMapHighlight : TileMap
{
    //public int width;
    //public int height;

    public Vector2I movementHighlightTile;
    public BattleMapHighlight(int width, int height)
    {
        Name = "Battle Map Highlight";

        // Hardcoded TileSet and specific highlight tile.
        Set(PropertyName.TileSet, ResourceLoader.Load("resources/tilesets/rtstilemap.tres", PropertyName.TileSet));
        movementHighlightTile = new Vector2I(14, 8);
    }

    private void PerformHighlighting(List<Vector2I> tiles)
    {
        foreach (var tile in tiles)
        {
            SetCell(0, tile, 0, movementHighlightTile);
        }
    }

}