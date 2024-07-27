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

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //GD.Print("Gonna try to get SignalManager.");
        signalManager = SignalManager.Instance;
        //signalManager = GetNode<SignalManager>("/root/Main/SignalManager");
        signalManager.C(SignalManager.SignalName.PerformBattleMapHighlightAdd.ToString(), this, nameof(PerformBattleMapHighlightAdd));
        signalManager.C(SignalManager.SignalName.PerformBattleMapHighlightRemoveAll.ToString(), this, nameof(PerformBattleMapHighlightRemoveAll));

    }

    /// <summary>
    /// Highlight all of the tiles in a PathMap.
    /// </summary>
    /// <param name="pathmap">The PathMap of tiles we'll be highlighting.</param>
    private void PerformBattleMapHighlightAdd(PathMap pathmap)
    {
        PerformBattleMapHighlightAdd(pathmap.ToVector2IList());
    }

    /// <summary>
    /// Highlight all of the tiles in a list of Vector2I.
    /// </summary>
    /// <param name="tiles">The BattleMapTiles we'll be highlighting.</param>
    private void PerformBattleMapHighlightAdd(List<Vector2I> tiles)
    {
        foreach (var tile in tiles)
        {
            SetCell(0, tile, 0, movementHighlightTile);
        }
    }

    /// <summary>
    /// If there's any highlights, Clear that whole layer.
    /// </summary>
    private void PerformBattleMapHighlightRemoveAll()
    {
        if (GetUsedCells(0).Count > 0)
        {
            Clear();
        }
    }
}