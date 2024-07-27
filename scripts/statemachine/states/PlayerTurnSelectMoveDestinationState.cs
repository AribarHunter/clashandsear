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
            CancelUnitSelection();
        }
    }

    public override void Enter()
    {
        base.Enter();
        GD.Print($"We're gonna do something with this unit: {GameContext.Instance.selectedActor.Name}");
        //signalManager.C(SignalManager.SignalName.PerformSelectUnitAction.ToString(), this, nameof(PerformSelectUnitAction));

    }

    public override void Exit()
    {
        base.Exit();
    }

    public void CancelUnitSelection()
    {
        GameContext.Instance.selectedActor = null;
        stateMachine.CurrentState = new PlayerTurnBaseState();

    }

    //protected void PerformSelectUnitAction(Actor actor)
    //{
    //    GD.Print("Hey this is where we'd switch states.");
    //}
}
