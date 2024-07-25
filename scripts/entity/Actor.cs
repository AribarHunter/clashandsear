using Godot;

public partial class Actor : Entity
{
    public int moveRange;

    public Actor(string name, Texture2D texture2D, int moveRange) : base(name, texture2D)
    {
        this.moveRange = moveRange;
    }

    /// <summary>
    /// Pathfinder helper to determine if a tile is within the Actor's move range.
    /// </summary>
    /// <param name="fromTile">The tile we'd move from.</param>
    /// <param name="toTile">The tile we'd move to.</param>
    /// <returns>True if the Actor can move between fromTile to toTile.</returns>
    public bool CanActorMoveBetweenTiles(BattleMapTile fromTile, BattleMapTile toTile)
    {
        return (fromTile.pathfindingDistance + 1) <= moveRange;
    }
}