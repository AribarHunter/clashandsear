using Godot;
using System.Collections.Generic;
using System.Linq;

public class BattleMapTile
{
    public List<Entity> entities = new();
    public List<Actor> Actors
    {
        get { return (List<Actor>)entities.OfType<Actor>(); }
    }
    public Vector2I position;
}