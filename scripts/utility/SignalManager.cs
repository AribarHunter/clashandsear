using Godot;
using System.Collections.Generic;

public partial class SignalManager : Node
{
    [Signal] public delegate void PerformConfirmActionEventHandler(Vector2I delta);
    [Signal] public delegate void PerformMoveActionEventHandler(Vector2I delta);
    [Signal] public delegate void PerformBattleMapHighlightAddEventHandler(PathMap tiles);
    [Signal] public delegate void PerformBattleMapHighlightRemoveAllEventHandler(PathMap tiles);
    [Signal] public delegate void PerformSelectUnitActionEventHandler(Vector2I delta);

    public static SignalManager Instance { get; private set; }

    public SignalManager(Node2D parentNode)
    {
        Name = "SignalManager";
        parentNode.AddChild(this);
    }

    public override void _Ready()
    {
        Instance = this;
    }

    /// <summary>
    /// Connects a signal to a method on the target object.
    /// </summary>
    /// <param name="signal">Signal to connect.</param>
    /// <param name="target">Target object.</param>
    /// <param name="method">Method to connect.</param>
    /// <param name="binds">Additional arguments I guess?</param>
    /// <param name="flags">Used to set deferred or one-shot. See ConnectFlag constants.</param>
    public void C(string signal, GodotObject target, string method, Godot.Collections.Array binds = null, uint flags = 0)
    {
        Connect(signal, new Callable(target, method), flags);
        // GD.Print($"Connected Signal: {signal} to {target.ToString()}.{method}");
    }

    /// <summary>
    /// Emits the given signal.
    /// </summary>
    /// <param name="signal">Signal to emit.</param>
    /// <param name="args">Additional arguments.</param>
    public void E(string signal, params Variant[] args)
    {
        EmitSignal(signal, args);
        //GD.Print($"Emit Signal: {signal}");
    }
}
