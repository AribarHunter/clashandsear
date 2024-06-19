using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// Everything needed to find paths or areas!
/// </summary>
public class Pathfinder
{
    static void ClearSearch(BattleMap map)
    {
        foreach (BattleMapTile tile in map.tiles)
        {
            tile.pathfindingDistance = int.MaxValue;
        }
    }

    public static PathMap SearchArea(BattleMap map, Vector2I startPosition, Func<BattleMapTile, BattleMapTile, bool> addTile)
    {
        PathMap result;
    }
}