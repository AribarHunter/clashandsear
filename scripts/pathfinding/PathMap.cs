using System.Collections.Generic;
using System.Linq;

public class PathMap
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
}