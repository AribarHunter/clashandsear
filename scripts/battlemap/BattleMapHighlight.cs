using Godot;
using System.Collections.Generic;

public partial class BattleMapHighlight : TileMap
{
    //public int width;
    //public int height;

    SignalManager signalManager;

    public Vector2I movementHighlightTile;
    public BattleMapHighlight(int width, int height)
    {
        Name = "Battle Map Highlight";

        // Hardcoded TileSet and specific highlight tile.
        Set(PropertyName.TileSet, ResourceLoader.Load("resources/tilesets/rtstilemap.tres", PropertyName.TileSet));
        movementHighlightTile = new Vector2I(14, 8);
    }

    public override void _Ready()
    {
        GD.Print("Gonna try to get SignalManager.");
        signalManager = GetNode<SignalManager>("/root/Main/SignalManager");
        signalManager.C(SignalManager.SignalName.PerformBattleMapHighlightAdd.ToString(), this, nameof(PerformBattleMapHighlightAdd));
    }

    private void PerformBattleMapHighlightAdd(PathMap pathmap)
    {
        List<Vector2I> tiles = pathmap.ToVector2IList();
        foreach (var tile in tiles)
        {
            SetCell(0, tile, 0, movementHighlightTile);
        }
    }

}