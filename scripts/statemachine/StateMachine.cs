using Godot;

public partial class StateMachine : Node
{
	protected State _currentState;
	protected bool _inTransition;
	public SignalManager signalManager;
	//public MainScene ms;

	public virtual State CurrentState
	{
		get { return _currentState; }
		set { TransitionTo(value); }
	}

	public StateMachine(Node parent, SignalManager signalManager)
	{
		Name = "StateMachine";
		this.signalManager = signalManager;
		parent.AddChild(this);
	}


	// Called when the node enters the scene tree for the first time.
	// public override void _Ready()
	// {
	// }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_currentState.Update();
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		base._UnhandledInput(@event);
		_currentState.HandleInput(@event);
	}

	/// <summary>
	/// This is used to transition from one State to another.
	/// </summary>
	/// <param name="value">The State we are transitioning to.</param>
	protected virtual void TransitionTo(State value)
	{
		if (_currentState == value || _inTransition)
		{
			GD.PushError(string.Format("State Transition problem! We were in {0}, tried going to {1}. _inTransition: {2}", _currentState.ToString(), value.ToString(), _inTransition));
			return;
		}
		_inTransition = true;
		if (_currentState != null)
		{
			_currentState.Exit();
		}
		_currentState = value;
		if (_currentState != null)
		{
			_currentState.signalManager = signalManager;
			_currentState.Enter();
		}
		_inTransition = false;
	}
}
