namespace ClashAndSear.scripts.pathfinding;

public class PathNode(battlemap.BattleMapTile previousTile, int costSoFar)
{
    public battlemap.BattleMapTile previousTile = previousTile;
    public int costSoFar = costSoFar;
}