using Godot;
using System.Collections.Generic;
using System.Linq;

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

        signalManager = SignalManager.Instance;
        signalManager.C(SignalManager.SignalName.PerformConfirmAction.ToString(), this, nameof(PerformConfirmAction));
        signalManager.C(SignalManager.SignalName.PerformMoveAction.ToString(), this, nameof(PerformMoveAction));
    }

    protected void PerformConfirmAction()
    {
        if (battleMap.DoesPositionContainActor(tilePosition))
        {
            List<Actor> actors = battleMap.GetActorsInPosition(tilePosition);
            GameContext.Instance.selectedActor = actors.First();
            signalManager.E(SignalManager.SignalName.PerformSelectUnitAction.ToString(), actors.First());
        }
    }

    /// <summary>
    /// Called when we need to move the BattleMapCursor.
    /// </summary>
    /// <param name="delta">The difference where we're moving.</param>
    protected void PerformMoveAction(Vector2I delta)
    {
        //GD.Print($"Old {tilePosition}, delta {delta}");
        tilePosition += delta;
        Set(PropertyName.Position, battleMap.MapToLocal(tilePosition));

        // Let's check if someone's under our tile?
        if (battleMap.DoesPositionContainActor(tilePosition))
        {
            List<Actor> actors = battleMap.GetActorsInPosition(tilePosition);
            GD.Print($"Hey there's an Actor here! It's {actors.First().Name}");
            PathMap areaToHighlight = Pathfinder.SearchArea(battleMap, actors.First().battleMapPosition, actors.First().CanActorMoveBetweenTiles);
            // Emit signal to create highlight?
            signalManager.E(SignalManager.SignalName.PerformBattleMapHighlightAdd.ToString(), areaToHighlight);
        }
        else
        {
            // Emit signal that there's nothing here?
            signalManager.E(SignalManager.SignalName.PerformBattleMapHighlightRemoveAll.ToString());
        }
    }
}
