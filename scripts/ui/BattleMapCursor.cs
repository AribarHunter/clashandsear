using Godot;

public partial class BattleMapCursor : Node2D
{
    BattleMap battleMap;
    SignalManager signalManager;

    Vector2I tilePosition;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        battleMap = GetParent<BattleMap>();
        Set(PropertyName.Position, battleMap.MapToLocal(Vector2I.Zero));
        tilePosition = battleMap.LocalToMap(Position);

        GD.Print("Gonna try to get SignalManager.");
        signalManager = GetNode<SignalManager>("/root/Main/SignalManager");
        signalManager.C(SignalManager.SignalName.PerformMoveAction.ToString(), this, nameof(PerformMoveAction));
    }

    protected void PerformMoveAction(Vector2I delta)
    {
        //GD.Print($"Old {tilePosition}, delta {delta}");
        tilePosition += delta;
        Set(PropertyName.Position, battleMap.MapToLocal(tilePosition));

        // Let's check if someone's under our tile?
        if (battleMap.DoesPositionContainEntity(tilePosition))
        {
            GD.Print("Hey there's something here!");
        }
    }
}
