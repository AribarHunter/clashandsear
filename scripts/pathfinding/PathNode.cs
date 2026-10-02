using ClashAndSear.scripts.battlemap;

namespace ClashAndSear.scripts.pathfinding
{
    public class PathNode(BattleMapTile previousTile, int costSoFar)
    {
        public BattleMapTile previousTile = previousTile;
        public int costSoFar = costSoFar;
    }
}