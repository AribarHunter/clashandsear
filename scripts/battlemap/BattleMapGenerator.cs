using System.Reflection.Metadata;
using Godot;

public partial class BattleMapGenerator : Node
{
    //Node2D parentNode;
    public BattleMapGenerator(Node2D parentNode)
    {
        Name = "LevelGenerator";
        parentNode.AddChild(this);
    }

    public BattleMap CreateBattleMap(string name)
    {
        BattleMap battleMap = new(10, 10, name);
        GetParent().AddChild(battleMap);
        battleMap.FillMapWithCheckerboard(new Vector2I(0, 0), new Vector2I(1, 1));
        return battleMap;
    }

    public void AddEntityToPosition(Entity entity, Vector2I position)
    {
        entity.currentPosition = position;
        entity.UpdateTransformToTile();
        GetParent().AddChild(entity);
    }
}