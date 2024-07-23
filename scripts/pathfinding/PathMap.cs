using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class PathMap : GodotObject
{
    public BattleMapTile startTile;
    public BattleMapTile endTile;
    public Dictionary<BattleMapTile, PathNode> valueToKeyPath = new Dictionary<BattleMapTile, PathNode>(); // New code what dis again?

    public PathMap(BattleMapTile startTile, BattleMapTile endTile)
    {
        this.startTile = startTile;
        this.endTile = endTile;
        valueToKeyPath.Add(startTile, new PathNode(null, 0)); // Argh what is this?
    }

    public List<BattleMapTile> ToTileList()
    {
        return valueToKeyPath.Keys.ToList();
    }

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