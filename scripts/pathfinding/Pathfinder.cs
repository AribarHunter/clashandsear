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
        // Prepare the starting Tile, the PathMap, Queues and clear previous pathfinding searches.
        BattleMapTile startTile = map.GetTileAt(startPosition);
        PathMap result = new PathMap(startTile, null);
        Queue<BattleMapTile> checkNext = new();
        Queue<BattleMapTile> checkNow = new();
        ClearSearch(map);

        startTile.pathfindingDistance = 0;
        checkNow.Enqueue(startTile);
        
        // While we have tiles to check, determine if they are in the area we're checking.
        while(checkNow.Count > 0)
        {
            // Take a tile from checkNow.
            BattleMapTile currentTile = checkNow.Dequeue();

            // Look at its neighbors and determine if they should be added.
            foreach(BattleMapTile potentialNextTile in map.GetCardinalNeighborTilesOf(currentTile))
            {
                // We only care about valid tiles that are equal or further away from the current tile. I think.
                if (potentialNextTile == null || potentialNextTile.pathfindingDistance <= currentTile.pathfindingDistance + 1)
                    continue;

                // Based on the results of addTile, we'll add the tile to the PathMap.
                if(addTile(currentTile, potentialNextTile))
                {
                    // Figure out its distance, then create a PathNode that we came from there.
                    potentialNextTile.pathfindingDistance = currentTile.pathfindingDistance + 1;
                    PathNode pathNode = new(potentialNextTile, potentialNextTile.pathfindingDistance);
                    // Add to the result and queue tiles for the next pass.
                    result.valueToKeyPath[potentialNextTile] = pathNode;
                    checkNext.Enqueue(potentialNextTile);
                }
            }

            //If we're out of tiles to check, we swap checkNow and checkNext.
            if (checkNow.Count == 0)
                SwapReference(ref checkNow, ref checkNext);
        }
        return result;
    }

    /// <summary>
    /// Swap two queues of BattleMapTiles.
    /// </summary>
    /// <param name="firstRef">First queue.</param>
    /// <param name="secondRef">Second queue.</param>
    static void SwapReference(ref Queue<BattleMapTile> firstRef, ref Queue<BattleMapTile> secondRef)
    {
        Queue<BattleMapTile> tempRef = firstRef;
        firstRef = secondRef;
        secondRef = tempRef;
    }
}