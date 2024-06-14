using Godot;
using System.Collections.Generic;

public class BattleMapTile
{
    public List<Entity> entities = new();
    public Vector2I position;
}