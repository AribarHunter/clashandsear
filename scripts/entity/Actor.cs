namespace ClashAndSear.scripts.entity
{
    public partial class Actor : Entity
    {
        private int _moveRange = 3;
        
        /// <summary>
        /// Pathfinder helper to determine if a tile is within the Actor's move range.
        /// </summary>
        /// <param name="fromTile">The tile we'd move from.</param>
        /// <param name="toTile">The tile we'd move to.</param>
        /// <returns>True if the Actor can move between fromTile to toTile.</returns>
        public bool CanActorMoveBetweenTiles(battlemap.BattleMapTile fromTile, battlemap.BattleMapTile toTile)
        {
            return (fromTile.pathfindingDistance + 1) <= _moveRange;
        }
    }
}