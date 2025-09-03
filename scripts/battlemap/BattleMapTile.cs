using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class BattleMapTile : GodotObject
{

    public static int PIXELSQUARESIZE = 16;
    public List<Entity> entities = new();
    public List<Actor> Actors
    {
        get
        {
            return entities.OfType<Actor>().ToList();
        }
    }
    public Vector2I position;
    public int pathfindingDistance = int.MaxValue;
}