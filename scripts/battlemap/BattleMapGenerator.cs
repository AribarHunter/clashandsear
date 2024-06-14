using Godot;

public partial class BattleMapGenerator : Node
{
    public BattleMapGenerator(Node2D parentNode)
    {
        Name = "LevelGenerator";
        parentNode.AddChild(this);
    }

    /// <summary>
    /// Generates a basic BattleMap and fills it with a simple checkboard pattern.
    /// </summary>
    /// <param name="name">The BattleMap's node name.</param>
    /// <returns></returns>
    public BattleMap CreateBattleMap(string name)
    {
        BattleMap battleMap = new(10, 10, name);
        GetParent().AddChild(battleMap);
        battleMap.FillMapWithCheckerboard(new Vector2I(0, 0), new Vector2I(1, 1));
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
}
