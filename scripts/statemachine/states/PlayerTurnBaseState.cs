using Godot;

public partial class PlayerTurnBaseState : State
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
	}

	public override void Enter()
	{
		base.Enter();
		// Let's do something.
		GD.Print("Starting Player Turn!");
	}

	public override void Exit()
	{
		base.Exit();
	}
}
