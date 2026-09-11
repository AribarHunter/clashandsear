using Godot;

namespace ClashAndSear
{
    public partial class BaseTurnState : State
    {
        public override void HandleInput(InputEvent @event)
        {
            base.HandleInput(@event);
            // Movement
            if (Input.IsActionJustPressed("primary_up"))
            {
                SignalManager.Instance.E(SignalManager.SignalName.PerformMoveAction, Vector2.Up);
            }
            else if (Input.IsActionJustPressed("primary_down"))
            {
                SignalManager.Instance.E(SignalManager.SignalName.PerformMoveAction, Vector2.Down);
            }
            else if (Input.IsActionJustPressed("primary_left"))
            {
                SignalManager.Instance.E(SignalManager.SignalName.PerformMoveAction, Vector2.Left);
            }
            else if (Input.IsActionJustPressed("primary_right"))
            {
                SignalManager.Instance.E(SignalManager.SignalName.PerformMoveAction, Vector2.Right);
            }
            else if (Input.IsActionJustPressed("confirm"))
            {
                GD.PrintRich("[b]HandleInput (BaseTurnState):[/b] Confirm Pressed. Emitting PerformConfirmAction signal.");
                SignalManager.Instance.E(SignalManager.SignalName.PerformConfirmAction);
            }
        }

        public override void Enter()
        {
            base.Enter();
            
            SignalManager.Instance.C(SignalManager.SignalName.PerformSelectUnitAction, this, nameof(PerformSelectUnitAction));
            SignalManager.Instance.E(SignalManager.SignalName.PerformHighlightIfHoveringOverActorAction);
        }

        public override void Exit()
        {
            base.Exit();
            SignalManager.Instance.D(SignalManager.SignalName.PerformSelectUnitAction, this, nameof(PerformSelectUnitAction));
        }

        protected void PerformSelectUnitAction(Actor actor)
        {
            stateMachine.CurrentState = new SelectMoveDestinationState();
        }
    }
}