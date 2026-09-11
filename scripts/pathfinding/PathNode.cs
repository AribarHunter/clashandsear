namespace ClashAndSear
{
    public class PathNode(BattleMapTile previousTile, int costSoFar)
    {
        public BattleMapTile previousTile = previousTile;
        public int costSoFar = costSoFar;
    }
}