using Godot;

public partial class BattleMapGenerator : Node
{
    public BattleMapGenerator(Node2D parentNode)
    {
        Name = "BattleMapGenerator";
        parentNode.AddChild(this);
    }

    /// <summary>
    /// Generates a basic BattleMap and fills it with a simple checkboard pattern.
    /// </summary>
    /// <param name="name">The BattleMap's node name.</param>
    /// <returns></returns>
    public BattleMap CreateBattleMap(string name)
    {
        BattleMapHighlight battleMapHighlight = new();
        BattleMap battleMap = new(10, 10, name, battleMapHighlight);
        GetParent().AddChild(battleMap);
        FillMapWithCheckerboard(battleMap, new Vector2I(0, 0), new Vector2I(1, 1));
        return battleMap;
    }

    /// <summary>
    /// Puts the Entity in a specific position on a board. I feel like this could be better or in a different place.
    /// </summary>
    /// <param name="entity">The Entity to be positioned.</param>
    /// <param name="position">The position on the BattleMap.</param>
    /// <param name="battleMapTile">The specific BattleMapTile they'll be on.</param>
    public void AddEntityToPosition(Entity entity, Vector2I position, BattleMapTile battleMapTile)
    {
        entity.battleMapPosition = position;
        entity.SetEntityToBattleMapTile(battleMapTile);
        entity.UpdateTransformToTile();
        GetParent().AddChild(entity);
    }

    /// <summary>
    /// Silly temporary method to fill a BattleMap with a checkerboard of two textures in its tileset.
    /// </summary>
    /// <param name="tileOne">The position of the first tile.</param>
    /// <param name="tileTwo">The position of the second tile.</param>
    public static void FillMapWithCheckerboard(BattleMap map, Vector2I tileOne, Vector2I tileTwo)
    {
        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                BattleMapTile newTile = new();
                newTile.position = new Vector2I(x, y);
                map.tiles[x, y] = newTile;

                if ((x + y) % 2 == 0)
                    map.SetCell(0, new Vector2I(x, y), 0, tileOne);
                else
                    map.SetCell(0, new Vector2I(x, y), 0, tileTwo);
            }
        }
    }





}
