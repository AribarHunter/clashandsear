using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class PathMap : GodotObject
{
    public BattleMapTile startTile;
    public BattleMapTile endTile;
    public Dictionary<BattleMapTile, PathNode> valueToKeyPath = new();

    public PathMap(BattleMapTile startTile, BattleMapTile endTile)
    {
        this.startTile = startTile;
        this.endTile = endTile;
        valueToKeyPath.Add(startTile, new PathNode(null, 0));
    }

    /// <summary>
    /// Converts valueToKeyPath into a List of BattleMapTiles.
    /// </summary>
    /// <returns>All BattleMapTiles in valueToKeyPath.</returns>
    public List<BattleMapTile> ToTileList()
    {
        return valueToKeyPath.Keys.ToList();
    }

    /// <summary>
    /// Converts valueToKeyPath into a List of Vector2I.
    /// </summary>
    /// <returns>All positions in valueToKeyPath.</returns>
    public List<Vector2I> ToVector2IList()
    {
        List<Vector2I> result = new List<Vector2I>();
        foreach (BattleMapTile tile in ToTileList())
        {
            result.Add(tile.position);
        }
        return result;
    }
}