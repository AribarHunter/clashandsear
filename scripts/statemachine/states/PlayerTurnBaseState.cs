using Godot;

public partial class PlayerTurnBaseState : State
{
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
