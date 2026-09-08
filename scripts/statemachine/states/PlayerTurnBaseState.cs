using Godot;

namespace ClashAndSear.scripts.statemachine.states;

public partial class PlayerTurnBaseState : State
{
    public override void HandleInput(InputEvent @event)
    {
        base.HandleInput(@event);
        // Movement
        if (Input.IsActionJustPressed("primary_up"))
        {
            signalManager.E(utility.SignalManager.SignalName.PerformMoveAction.ToString(), Vector2.Up);
        }
        else if (Input.IsActionJustPressed("primary_down"))
        {
            signalManager.E(utility.SignalManager.SignalName.PerformMoveAction.ToString(), Vector2.Down);
        }
        else if (Input.IsActionJustPressed("primary_left"))
        {
            signalManager.E(utility.SignalManager.SignalName.PerformMoveAction.ToString(), Vector2.Left);
        }
        else if (Input.IsActionJustPressed("primary_right"))
        {
            signalManager.E(utility.SignalManager.SignalName.PerformMoveAction.ToString(), Vector2.Right);
        }
        else if (Input.IsActionJustPressed("confirm"))
        {
            GD.PrintRich("[b]HandleInput (PlayerTurnBaseState):[/b] Confirm Pressed. Emitting PerformConfirmAction signal.");
            signalManager.E(utility.SignalManager.SignalName.PerformConfirmAction.ToString());
        }
    }

    public override void Enter()
    {
        base.Enter();
        stateName = StateName.PlayerTurnBaseState;
        // Let's do something.
        //GD.Print("Starting Player Turn!");
        signalManager.C(utility.SignalManager.SignalName.PerformSelectUnitAction.ToString(), this, nameof(PerformSelectUnitAction));

        signalManager.E(utility.SignalManager.SignalName.PerformHighlightIfHoveringOverActorAction.ToString());

    }

    public override void Exit()
    {
        base.Exit();
        signalManager.D(utility.SignalManager.SignalName.PerformSelectUnitAction.ToString(), this, nameof(PerformSelectUnitAction));
    }

    protected void PerformSelectUnitAction(entity.Actor actor)
    {
        stateMachine.CurrentState = new PlayerTurnSelectMoveDestinationState();
    }
}