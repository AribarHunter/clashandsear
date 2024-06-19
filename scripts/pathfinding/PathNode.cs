public class PathNode
{
    public BattleMapTile previousTile;
    public int costSoFar;
    
    public PathNode(BattleMapTile previousTile, int costSoFar)
    {
        this.previousTile = previousTile;
        this.costSoFar = costSoFar;
    }
}