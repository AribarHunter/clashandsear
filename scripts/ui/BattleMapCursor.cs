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
        signalManager.C(SignalManager.SignalName.PerformHighlightIfHoveringOverActorAction.ToString(), this, nameof(PerformHighlightIfHoveringOverActorAction));
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

        Vector2I newPosition = tilePosition + delta;
        if (battleMap.PositionIsInbound(newPosition))
        {
            tilePosition = newPosition;
            Set(PropertyName.Position, battleMap.MapToLocal(tilePosition));

            if (GameContext.Instance.currentState.stateName == StateName.PlayerTurnBaseState)
            {
                PerformHighlightIfHoveringOverActorAction();
            }
            else if (GameContext.Instance.currentState.stateName == StateName.PlayerTurnSelectMoveDestinationState)
            {
                GD.Print("We'll do something here.");
            }
        }
        GD.PrintRich("A* Position InBounds?: {0} {1}", newPosition.ToString(), battleMap.astarGrid.IsInBoundsv(newPosition));
    }

    /// <summary>
    /// Removes all highlights, then if an actor is in the current position it adds a highlight.
    /// </summary>
    void PerformHighlightIfHoveringOverActorAction()
    {
        signalManager.E(SignalManager.SignalName.PerformBattleMapHighlightRemoveAll.ToString());
        if (battleMap.DoesPositionContainActor(tilePosition))
        {
            List<Actor> actors = battleMap.GetActorsInPosition(tilePosition);
            PathMap areaToHighlight = Pathfinder.SearchArea(battleMap, actors.First().battleMapPosition, actors.First().CanActorMoveBetweenTiles);

            signalManager.E(SignalManager.SignalName.PerformBattleMapHighlightAdd.ToString(), areaToHighlight);
        }
    }
}
