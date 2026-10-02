using Godot;

namespace ClashAndSear.scripts.battlemap
{
    public partial class BattleMapTile : GodotObject
    {
        public Vector2I position;
        public int pathfindingDistance = int.MaxValue;
    }
}