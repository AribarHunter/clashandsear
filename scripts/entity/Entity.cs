using Godot;

namespace ClashAndSear.scripts.entity;

public partial class Entity : Node2D
{

    public Vector2I battleMapPosition;
    private BattleMapTile _currentBattleMapTile;

    /// <summary>
    /// Use this to remove the Entity from its current BattleMapTile and add it to a new one.
    /// </summary>
    /// <param name="newBattleMapTile">The Entity's destination BattleMapTile.</param>
    public void SetEntityToBattleMapTile(BattleMapTile newBattleMapTile)
    {
        // Remove entity from current tile.
        _currentBattleMapTile?.entities.Remove(this);
        // Add entity to new tile.
        newBattleMapTile.entities.Add(this);
    }

    public void UpdateTransformToTile()
    {
        Position = CoordinateConverter.FindPixelAtTile(battleMapPosition, 0, 0);
    }
}