using Godot;

namespace ClashAndSear.scripts.statemachine;

public sealed partial class StateMachine : Node
{
    private State _currentState;
    private bool _inTransition;

    private utility.SignalManager _signalManager;

    public State CurrentState
    {
        get => _currentState;
        set => TransitionTo(value);
    }

    public StateMachine(Node parent, utility.SignalManager signalManager)
    {
        Name = "StateMachine";
        this._signalManager = signalManager;
        parent.AddChild(this);
    }

    public StateMachine()
    {
    }

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
    private void TransitionTo(State value)
    {
        if (_currentState == value || _inTransition)
        {
            GD.PushError(
                $"State Transition problem! We were in {_currentState}, tried going to {value}. _inTransition: {_inTransition}");
            return;
        }
        _inTransition = true;
        _currentState?.Exit();
        _currentState = value;
        if (_currentState != null)
        {
            _currentState.signalManager = _signalManager;
            _currentState.stateMachine = this;
            utility.GameContext.Instance.currentState = _currentState;
            _currentState.Enter();
        }
        _inTransition = false;
    }
}