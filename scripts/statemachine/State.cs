using Godot;

public abstract partial class State : GodotObject
{
    //State machine
    //signal manager

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
        //Assign state machine.
        //And signal manager.
    }

    /// <summary>
    /// This is called when exiting this State.
    /// Remember that you can't transition to a new state here!
    /// </summary>
    public virtual void Exit() { }
}
