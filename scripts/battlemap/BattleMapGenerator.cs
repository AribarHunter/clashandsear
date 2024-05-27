using Godot;

public partial class BattleMapGenerator : Node
{
    //Node2D parentNode;
    public BattleMapGenerator(Node2D parentNode)
    {
        Name = "LevelGenerator";
        //this.parentNode = parentNode;
        // GD.Print($"We're in the constructor!");
        parentNode.AddChild(this);
    }

    public void CreateBattleMap(string name) {
        BattleMap battleMap = new(10, 10);
        battleMap.Name = name;
        GetParent().AddChild(battleMap);
        battleMap.FillMapWithTileSetTile(new Vector2I(1, 0));
    }


}