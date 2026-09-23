using Godot;

namespace ClashAndSear.scripts.statemachine.states
{
    public partial class SelectMoveDestinationState : State
    {

        public override void HandleInput(InputEvent @event)
        {
            base.HandleInput(@event);
            // Movement
            if (Input.IsActionJustPressed("primary_up"))
            {
                SignalManager.Instance.E(SignalManager.SignalName.PerformMoveAction.ToString(), Vector2.Up);
            }
            else if (Input.IsActionJustPressed("primary_down"))
            {
                SignalManager.Instance.E(SignalManager.SignalName.PerformMoveAction.ToString(), Vector2.Down);
            }
            else if (Input.IsActionJustPressed("primary_left"))
            {
                SignalManager.Instance.E(SignalManager.SignalName.PerformMoveAction.ToString(), Vector2.Left);
            }
            else if (Input.IsActionJustPressed("primary_right"))
            {
                SignalManager.Instance.E(SignalManager.SignalName.PerformMoveAction.ToString(), Vector2.Right);
            }
            else if (Input.IsActionJustPressed("cancel"))
            {
                // GD.PrintRich("[b]HandleInput (SelectMoveDestinationState):[/b] Cancel pressed.");
                UnitSelectionWasCancelled();
            }
            else if (Input.IsActionJustPressed("confirm"))
            {
                // GD.PrintRich("[b]HandleInput (BaseTurnState):[/b] Confirm Pressed. Emitting PerformConfirmAction signal.");
                SignalManager.Instance.E(SignalManager.SignalName.PerformConfirmAction);
            }
        }

        public override void Enter()
        {
            base.Enter();
            SignalManager.Instance.C(SignalManager.SignalName.PerformSelectMoveDestination, this, nameof(PerformSelectMoveDestinationAction));
        }
        
        public override void Exit()
        {
            base.Exit();
            SignalManager.Instance.D(SignalManager.SignalName.PerformSelectMoveDestination, this, nameof(PerformSelectMoveDestinationAction));
        }

        /// <summary>
        /// Called when the user cancels selecting the unit.
        /// </summary>
        private void UnitSelectionWasCancelled()
        {
            GameContext.Instance.selectedActor = null;
            GameContext.Instance.stateMachine.CurrentState  = new BaseTurnState();
        }
        
        protected void PerformSelectMoveDestinationAction(Actor actor)
        {
            // GD.PrintRich("Here is where we'd advance state.");
            // TODO: Eventually make more me-friendly Pathfinder stuff...
            // PathMap areaToHighlight = Pathfinder.SearchArea(_battleMap, GameContext.Instance.selectedActor.battleMapPosition, GameContext.Instance.selectedActor.CanActorMoveBetweenTiles);
            if (true)
            {
                SignalManager.Instance.E(SignalManager.SignalName.PerformBattleMapHighlightRemoveAll);
                stateMachine.CurrentState = new UnitMovingState();
                
            }
            //
        }
    }
}