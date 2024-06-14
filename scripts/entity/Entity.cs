using Godot;

public partial class Entity : Sprite2D
{

    public Vector2I battleMapPosition;
    public BattleMapTile currentBattleMapTile;
    public Entity(string name, Texture2D texture2D)
    {
        Name = name;
        Texture = texture2D;
        Set(PropertyName.Texture, texture2D);
        Centered = false;
    }

    /// <summary>
    /// Use this to remove the Entity from its current BattleMapTile and add it to a new one.
    /// </summary>
    /// <param name="newBattleMapTile">The Entity's destination BattleMapTile.</param>
    public void SetEntityToBattleMapTile(BattleMapTile newBattleMapTile)
    {
        // Remove entity from current tile.
        currentBattleMapTile?.entities.Remove(this);
        // Add entity to new tile.
        newBattleMapTile.entities.Add(this);
    }

    public void UpdateTransformToTile()
    {
        Position = CoordinateConverter.FindPixelAtTile(battleMapPosition, 0, 0);
    }
}
