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
        
        /// <summary>
        /// Takes a position. If the selected actor can move to it, move onward. Otherwise do nothing.
        /// </summary>
        /// <param name="position">The position we will check.</param>
        protected void PerformSelectMoveDestinationAction(Vector2I position)
        {
            if (!GameContext.Instance.battleMap.CanActorMoveToPosition(GameContext.Instance.selectedActor, position))
                return;
            GameContext.Instance.selectedPosition = position;
            SignalManager.Instance.E(SignalManager.SignalName.PerformBattleMapHighlightRemoveAll);
            stateMachine.CurrentState = new UnitMovingState();
        }
    }
}