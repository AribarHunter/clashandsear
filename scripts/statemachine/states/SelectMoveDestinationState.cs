using Godot;

namespace ClashAndSear
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
                GD.PrintRich("[b]HandleInput (SelectMoveDestinationState):[/b] Cancel pressed.");
                UnitSelectionWasCancelled();
            }
        }

        public override void Enter()
        {
            base.Enter();
        }

        /// <summary>
        /// Called when the user cancels selecting the unit.
        /// </summary>
        private void UnitSelectionWasCancelled()
        {
            GameContext.Instance.selectedActor = null;
            GameContext.Instance.stateMachine.CurrentState  = new BaseTurnState();
        }
    }
}