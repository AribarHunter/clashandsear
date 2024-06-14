using Godot;

public partial class BattleMap : TileMap
{

    public int width;
    public int height;
    public BattleMapTile[,] tiles;

    public BattleMap(int width, int height, string name)
    {
        // Set up tile stuff.
        this.height = height;
        this.width = width;
        Name = name;
        tiles = new BattleMapTile[width, height];

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
    internal bool DoesPositionContainEntity(Vector2I tilePosition)
    {
        if (tiles[tilePosition.X, tilePosition.Y].entities.Count > 0)
            return true;
        return false;
    }
}