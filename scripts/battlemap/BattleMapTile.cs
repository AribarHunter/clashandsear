using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class BattleMapTile : GodotObject
{
    public List<ClashAndSear.scripts.entity.Entity> entities = new();
    public List<ClashAndSear.scripts.entity.Actor> Actors
    {
        get
        {
            return entities.OfType<ClashAndSear.scripts.entity.Actor>().ToList();
        }
    }
    public Vector2I position;
    public int pathfindingDistance = int.MaxValue;
}