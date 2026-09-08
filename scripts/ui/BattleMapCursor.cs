using System;
using System.Collections.Generic;
using System.Linq;
using ClashAndSear.scripts.pathfinding;
using ClashAndSear.scripts.statemachine;
using Godot;

namespace ClashAndSear.scripts.ui;

public partial class BattleMapCursor : Node2D
{
    private battlemap.BattleMap _battleMap;
    private utility.SignalManager _signalManager;

    private Vector2I _tilePosition;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _battleMap = GetParent<battlemap.BattleMap>();
        Set(Node2D.PropertyName.Position, _battleMap.MapToLocal(Vector2I.Zero));
        _tilePosition = _battleMap.LocalToMap(Position);

        _signalManager = utility.SignalManager.Instance;
        _signalManager.C(utility.SignalManager.SignalName.PerformConfirmAction.ToString(), this, nameof(PerformConfirmAction));
        _signalManager.C(utility.SignalManager.SignalName.PerformHighlightIfHoveringOverActorAction.ToString(), this, nameof(PerformHighlightIfHoveringOverActorAction));
        _signalManager.C(utility.SignalManager.SignalName.PerformMoveAction.ToString(), this, nameof(PerformMoveAction));
    }

    protected void PerformConfirmAction()
    {
        if (!_battleMap.DoesPositionContainActor(_tilePosition)) return;
        List<entity.Actor> actors = _battleMap.GetActorsInPosition(_tilePosition);
        utility.GameContext.Instance.selectedActor = actors.First();
        _signalManager.E(utility.SignalManager.SignalName.PerformSelectUnitAction.ToString(), actors.First());
    }

    /// <summary>
    /// Called when we need to move the BattleMapCursor.
    /// </summary>
    /// <param name="delta">The difference where we're moving.</param>
    protected void PerformMoveAction(Vector2I delta)
    {

        Vector2I newPosition = _tilePosition + delta;
        if (_battleMap.PositionIsInbound(newPosition))
        {
            _tilePosition = newPosition;
            Set(Node2D.PropertyName.Position, _battleMap.MapToLocal(_tilePosition));

            switch (utility.GameContext.Instance.currentState.stateName)
            {
                case StateName.PlayerTurnBaseState:
                    PerformHighlightIfHoveringOverActorAction();
                    break;
                case StateName.PlayerTurnSelectMoveDestinationState:
                    GD.Print("We'll do something here.");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        GD.PrintRich("A* Position InBounds?: {0} {1}", newPosition.ToString(), _battleMap.astarGrid.IsInBoundsv(newPosition));
    }

    /// <summary>
    /// Removes all highlights, then if an actor is in the current position it adds a highlight.
    /// </summary>
    private void PerformHighlightIfHoveringOverActorAction()
    {
        _signalManager.E(utility.SignalManager.SignalName.PerformBattleMapHighlightRemoveAll.ToString());
        if (!_battleMap.DoesPositionContainActor(_tilePosition)) return;
        List<entity.Actor> actors = _battleMap.GetActorsInPosition(_tilePosition);
        pathfinding.PathMap areaToHighlight = Pathfinder.SearchArea(_battleMap, actors.First().battleMapPosition, actors.First().CanActorMoveBetweenTiles);

        _signalManager.E(utility.SignalManager.SignalName.PerformBattleMapHighlightAdd.ToString(), areaToHighlight);
    }
}