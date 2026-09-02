using System.Collections.Generic;
using Godot;

namespace ClashAndSear.scripts.battlemap;

public partial class BattleMapHighlight : TileMapLayer
{
    //public int width;
    //public int height;

    private SignalManager _signalManager;
    private Vector2I _movementHighlightTile;

    public BattleMapHighlight()
    {
        Name = "Battle Map Highlight";

        // Hardcoded TileSet and specific highlight tile.
        Set(TileMapLayer.PropertyName.TileSet, ResourceLoader.Load("resources/tilesets/rtstilemap.tres", TileMapLayer.PropertyName.TileSet));
        _movementHighlightTile = new Vector2I(14, 8);
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _signalManager = SignalManager.Instance;
        _signalManager.C(SignalManager.SignalName.PerformBattleMapHighlightAdd.ToString(), this, nameof(PerformBattleMapHighlightAdd));
        _signalManager.C(SignalManager.SignalName.PerformBattleMapHighlightRemoveAll.ToString(), this, nameof(PerformBattleMapHighlightRemoveAll));
    }

    /// <summary>
    /// Highlight all the tiles in a PathMap.
    /// </summary>
    /// <param name="pathmap">The PathMap of tiles we'll be highlighting.</param>
    private void PerformBattleMapHighlightAdd(pathfinding.PathMap pathmap)
    {
        PerformBattleMapHighlightAdd(pathmap.ToVector2IList());
    }

    /// <summary>
    /// Highlight all the tiles in a list of Vector2I.
    /// </summary>
    /// <param name="tiles">The BattleMapTiles we'll be highlighting.</param>
    private void PerformBattleMapHighlightAdd(List<Vector2I> tiles)
    {
        foreach (Vector2I tile in tiles)
        {
            SetCell(tile, 0, _movementHighlightTile);
        }
    }

    /// <summary>
    /// If there's any highlights, Clear that whole layer.
    /// </summary>
    private void PerformBattleMapHighlightRemoveAll()
    {
        if (GetUsedCells().Count > 0)
        {
            Clear();
        }
    }
}