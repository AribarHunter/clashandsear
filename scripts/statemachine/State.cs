using Godot;

public abstract partial class State : GodotObject
{
    public SignalManager signalManager;
    public StateMachine stateMachine;

    internal StateName stateName;

    /// <summary>
    /// The StateMachine will call this in _Process.
    /// </summary>
    public virtual void Update() { }

    /// <summary>
    /// The StateMachine will call this in _UnhandledInput.
    /// </summary>
    /// <param name="event"></param>
    public virtual void HandleInput(InputEvent @event) { }

    /// <summary>
    /// This is called when entering this State.
    /// Remember that you can't transition to a new state here!
    /// </summary>
    public virtual void Enter()
    {
        GD.Print($"Entering State: {this.GetType().Name}");
    }

    /// <summary>
    /// This is called when exiting this State.
    /// Remember that you can't transition to a new state here!
    /// </summary>
    public virtual void Exit()
    {
        GD.Print($"Exiting State: {this.GetType().Name}");
    }
}
