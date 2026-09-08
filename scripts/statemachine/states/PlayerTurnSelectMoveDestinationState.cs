using Godot;

namespace ClashAndSear.scripts.statemachine.states;

public partial class PlayerTurnSelectMoveDestinationState : State
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

    /// <summary>
    /// Called when the user cancels selecting the unit.
    /// </summary>
    private void UnitSelectionWasCancelled()
    {
        utility.GameContext.Instance.selectedActor = null;
        stateMachine.CurrentState = new PlayerTurnBaseState();
    }
}