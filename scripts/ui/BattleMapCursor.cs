using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace ClashAndSear.scripts.ui
{
    public partial class BattleMapCursor : Node2D
    {
        private BattleMap _battleMap;
        private SignalManager _signalManager;

        private Vector2I _tilePosition;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            _battleMap = GetParent<BattleMap>();
            Set(Node2D.PropertyName.Position, _battleMap.MapToLocal(Vector2I.Zero));
            _tilePosition = _battleMap.LocalToMap(Position);

            _signalManager = SignalManager.Instance;
            _signalManager.C(SignalManager.SignalName.PerformConfirmAction, this, nameof(PerformConfirmAction));
            _signalManager.C(SignalManager.SignalName.PerformHighlightIfHoveringOverActorAction, this, nameof(PerformHighlightIfHoveringOverActorAction));
            _signalManager.C(SignalManager.SignalName.PerformMoveAction, this, nameof(PerformMoveAction));
        }

        protected void PerformConfirmAction()
        {
            switch (GameContext.Instance.stateMachine.CurrentState)
            {
                case BaseTurnState:
                    if (!_battleMap.DoesPositionContainActor(_tilePosition)) return;
                    List<Actor> actors = _battleMap.GetActorsInPosition(_tilePosition);
                    GameContext.Instance.selectedActor = actors.First();
                    _signalManager.E(SignalManager.SignalName.PerformSelectUnitAction, GameContext.Instance.selectedActor);
                    break;
                case SelectMoveDestinationState:
                    _signalManager.E(SignalManager.SignalName.PerformSelectMoveDestination, _tilePosition);
                    break;
                default:
                    return;
            }
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

                switch (GameContext.Instance.stateMachine.CurrentState)
                {
                    case BaseTurnState:
                        PerformHighlightIfHoveringOverActorAction();
                        break;
                    case SelectMoveDestinationState:
                        // GD.Print("We'll do something here.");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            // GD.PrintRich("A* Position InBounds?: {0} {1}", newPosition.ToString(), _battleMap.astarGrid.IsInBoundsv(newPosition));
        }

        /// <summary>
        /// Removes all highlights, then if an actor is in the current position it adds a highlight.
        /// </summary>
        private void PerformHighlightIfHoveringOverActorAction()
        {
            _signalManager.E(SignalManager.SignalName.PerformBattleMapHighlightRemoveAll);
            if (!_battleMap.DoesPositionContainActor(_tilePosition)) return;
            List<Actor> actors = _battleMap.GetActorsInPosition(_tilePosition);
            PathMap areaToHighlight = Pathfinder.SearchArea(_battleMap, actors.First().battleMapPosition, actors.First().CanActorMoveBetweenTiles);

            _signalManager.E(SignalManager.SignalName.PerformBattleMapHighlightAdd, areaToHighlight);
        }
    }
}