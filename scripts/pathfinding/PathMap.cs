using System.Collections.Generic;
using System.Linq;
using ClashAndSear.scripts.battlemap;
using Godot;

namespace ClashAndSear.scripts.pathfinding
{
    public partial class PathMap : GodotObject
    {
        private BattleMapTile _startTile;
        private BattleMapTile _endTile;
        public readonly Dictionary<BattleMapTile, PathNode> valueToKeyPath = new();

        public PathMap(BattleMapTile startTile, BattleMapTile endTile)
        {
            _startTile = startTile;
            _endTile = endTile;
            valueToKeyPath.Add(startTile, new PathNode(null, 0));
        }

        public PathMap()
        {
        }

        /// <summary>
        /// Converts valueToKeyPath into a List of BattleMapTiles.
        /// </summary>
        /// <returns>All BattleMapTiles in valueToKeyPath.</returns>
        private List<BattleMapTile> ToTileList()
        {
            return valueToKeyPath.Keys.ToList();
        }

        /// <summary>
        /// Converts valueToKeyPath into a List of Vector2I.
        /// </summary>
        /// <returns>All positions in valueToKeyPath.</returns>
        public List<Vector2I> ToVector2IList()
        {
            return ToTileList().Select(tile => tile.position).ToList();
        }
    }
}