using Godot;
using System.Collections.Generic;

public partial class BattleMap : TileMap
{

    public int width;
    public int height;
    public BattleMapTile[,] tiles;
    public BattleMapHighlight battleMapHighlight;

    
    readonly Vector2I[] cardinalNeighbors = new Vector2I[4]
    {
        new(0, 1),
        new(0, -1),
        new(1, 0),
        new(-1, 0)
    };

    public BattleMap(int width, int height, string name, BattleMapHighlight battleMapHighlight)
    {
        // Set up tile stuff.
        this.height = height;
        this.width = width;
        Name = name;
        tiles = new BattleMapTile[width, height];

        // Set up BattleMapHighlight.
        this.battleMapHighlight = battleMapHighlight;
        AddChild(battleMapHighlight);

        // Hardcoded TileSet for now.
        Set(PropertyName.TileSet, ResourceLoader.Load("resources/tilesets/rtstilemap.tres", PropertyName.TileSet));
    }

    /// <summary>
    /// Silly temporary method to fill a BattleMap with a checkerboard of two textures in its tileset.
    /// </summary>
    /// <param name="tileOne">The position of the first tile.</param>
    /// <param name="tileTwo">The position of the second tile.</param>
    public void FillMapWithCheckerboard(Vector2I tileOne, Vector2I tileTwo)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                BattleMapTile newTile = new();
                newTile.position = new Vector2I(x, y);
                tiles[x, y] = newTile;

                if ((x + y) % 2 == 0)
                    SetCell(0, new Vector2I(x, y), 0, tileOne);
                else
                    SetCell(0, new Vector2I(x, y), 0, tileTwo);
            }
        }
    }

    /// <summary>
    /// Checks if there are any Entities recorded a BattleMapTile found by its position.
    /// </summary>
    /// <param name="tilePosition">The position of the BattleMapTile.</param>
    /// <returns></returns>
    public bool DoesPositionContainEntity(Vector2I tilePosition)
    {
        if (tiles[tilePosition.X, tilePosition.Y].entities.Count > 0)
            return true;
        return false;
    }

    public bool DoesPositionContainActor(Vector2I tilePosition)
    {
        if (tiles[tilePosition.X, tilePosition.Y].Actors.Count > 0)
            return true;
        return false;
    }

    /// <summary>
    /// Retrieves a list of actors.
    /// </summary>
    /// <param name="position">The Tile position to be checked.</param>
    /// <returns>A list of all Actors in the tile position.</returns>
    public List<Actor> GetActorsInPosition(Vector2I position)
    {
        return tiles[position.X, position.Y].Actors;
    }

    /// <summary>
    /// Returns
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public BattleMapTile GetTileAt(Vector2I position)
    {
        if(PositionIsInbound(position))
            return tiles[position.X, position.Y];
        return null;
    }

    /// <summary>
    /// Returns the cardinal neighbors BattleMapTiles of a certain position.
    /// </summary>
    /// <param name="position">The position of the BattleMapTile we're checking.</param>
    /// <returns>The neighboring BattleMapTiles. Note this only returns inbound tiles!</returns>
    public List<BattleMapTile> GetCardinalNeighborTilesOf(Vector2I position)
    {
        List<BattleMapTile> result = new();
        for (int i = 0; i < 4; i++)
            result.Add(GetTileAt(position + cardinalNeighbors[i]));
        return result;
    }

    public List<BattleMapTile> GetCardinalNeighborTilesOf(BattleMapTile tile)
    {
        return GetCardinalNeighborTilesOf(tile.position);
    }

   /// <summary>
   /// Checks if a position is inside the bounds of this level.
   /// </summary>
   /// <param name="position">The position we're checking.</param>
   /// <returns>True for inbounds, false for outside.</returns>
    public bool PositionIsInbound(Vector2I position)
    {
        return (0 <= position.X && position.X < width &&
                0 <= position.Y && position.Y < height);
    }
}