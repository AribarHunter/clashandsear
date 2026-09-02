using System.Collections.Generic;
using System.Linq;
using Godot;

namespace ClashAndSear.scripts.battlemap;

public partial class BattleMapTile : GodotObject
{
    public readonly List<entity.Entity> entities = [];
    public List<entity.Actor> Actors => entities.OfType<entity.Actor>().ToList();
    public Vector2I position;
    public int pathfindingDistance = int.MaxValue;
}