using Godot;

public partial class PlayerTurnSelectMoveDestinationState : State
{

    public override void HandleInput(InputEvent @event)
    {
        base.HandleInput(@event);
        // Movement
        if (Input.IsActionJustPressed("primary_up"))
        {
            signalManager.E(SignalManager.SignalName.PerformMoveAction.ToString(), Vector2.Up);
        }
        else if (Input.IsActionJustPressed("primary_down"))
        {
            signalManager.E(SignalManager.SignalName.PerformMoveAction.ToString(), Vector2.Down);
        }
        else if (Input.IsActionJustPressed("primary_left"))
        {
            signalManager.E(SignalManager.SignalName.PerformMoveAction.ToString(), Vector2.Left);
        }
        else if (Input.IsActionJustPressed("primary_right"))
        {
            signalManager.E(SignalManager.SignalName.PerformMoveAction.ToString(), Vector2.Right);
        }
        else if (Input.IsActionJustPressed("cancel"))
        {
            GD.PrintRich("[b]HandleInput (PlayerTurnSelectMoveDestinationState):[/b] Cancel pressed.");
            UnitSelectionWasCancelled();
        }
    }

    public override void Enter()
    {
        base.Enter();
        stateName = StateName.PlayerTurnSelectMoveDestinationState;
    }

    public override void Exit()
    {
        base.Exit();
    }

    /// <summary>
    /// Called when the user cancels selecting the unit.
    /// </summary>
    public void UnitSelectionWasCancelled()
    {
        GameContext.Instance.selectedActor = null;
        stateMachine.CurrentState = new PlayerTurnBaseState();
    }
}
