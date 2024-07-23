using Godot;

public partial class Actor : Entity
{
    public int moveRange;

    public Actor(string name, Texture2D texture2D, int moveRange) : base(name, texture2D)
    {
        this.moveRange = moveRange;
    }

    public bool AddTile(BattleMapTile fromTile, BattleMapTile toTile)
    {
        return (fromTile.pathfindingDistance + 1) <= moveRange;
    }
}