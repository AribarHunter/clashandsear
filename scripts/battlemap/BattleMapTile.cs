using System.Collections.Generic;
using System.Linq;
using Godot;

namespace ClashAndSear
{
    public partial class BattleMapTile : GodotObject
    {
        public readonly List<Entity> entities = [];
        public List<Actor> Actors => entities.OfType<Actor>().ToList();
        public Vector2I position;
        public int pathfindingDistance = int.MaxValue;
    }
}