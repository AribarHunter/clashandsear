using System.Threading.Tasks;
using GdUnit4;
using Godot;
using static GdUnit4.Assertions;

// ReSharper disable UnusedMember.Global

namespace ClashAndSear.scripts.ui;

[TestSuite]
public class BattleMapCursorTest
{
    /// <summary>
    /// TODO: Finish test case.
    /// Given the BattleMapCursor receives a PerformConfirmAction signal
    ///     When the game is in the BaseTurnState
    ///     And the position contains one actor
    ///         Then emit a PerformSelectUnitAction with that actor as the argument.
    /// </summary>
    /// <returns></returns>
    [TestCase]
    [RequireGodotRuntime]
    public static async Task TestPerformConfirmActionInBaseTurnStateSingleActor()
    {
        ISceneRunner runner = ISceneRunner.Load("res://scenes/test/gdUnit4TestScene_workpad.tscn");
        BattleMapGenerator battleMapGenerator = runner.Scene()!.GetNode<BattleMapGenerator>("%BattleMapGenerator");
        BattleMap testMap = battleMapGenerator.CreateBattleMap("TestMap");
        PackedScene unitPackedScene = GD.Load<PackedScene>("res://scenes/entities/actor.tscn");
        Actor unit = unitPackedScene.Instantiate<Actor>();
        battleMapGenerator.AddEntityToPosition(unit, new Vector2I(0, 0), testMap.tiles[0, 0]);
        AssertSignal(SignalManager.Instance).StartMonitoring();
        
        SignalManager.Instance.E(SignalManager.SignalName.PerformConfirmAction);
        
        await AssertSignal(SignalManager.Instance)
            .IsEmitted(SignalManager.SignalName.PerformSelectUnitAction, unit)
            .WithTimeout(50);
    }
    
    /// <summary>
    /// TODO: Finish test case.
    /// Given the BattleMapCursor receives a PerformConfirmAction signal
    ///     When the game is in the BaseTurnState
    ///     And the position contains more than one actor
    ///         Then emit a PerformSelectUnitAction with the first actor as the argument.
    /// </summary>
    /// <returns></returns>
    [TestCase]
    [RequireGodotRuntime]
    public static async Task TestPerformConfirmActionInBaseTurnStateMultipleActors()
    {
        ISceneRunner runner = ISceneRunner.Load("res://scenes/test/gdUnit4TestScene_workpad.tscn");
        BattleMapGenerator battleMapGenerator = runner.Scene()!.GetNode<BattleMapGenerator>("%BattleMapGenerator");
        BattleMap testMap = battleMapGenerator.CreateBattleMap("TestMap");
        PackedScene unitPackedScene = GD.Load<PackedScene>("res://scenes/entities/actor.tscn");
        Actor unit1 = unitPackedScene.Instantiate<Actor>();
        battleMapGenerator.AddEntityToPosition(unit1, new Vector2I(0, 0), testMap.tiles[0, 0]);
        Actor unit2 = unitPackedScene.Instantiate<Actor>();
        battleMapGenerator.AddEntityToPosition(unit2, new Vector2I(0, 0), testMap.tiles[0, 0]);
        AssertSignal(SignalManager.Instance).StartMonitoring();
        
        SignalManager.Instance.E(SignalManager.SignalName.PerformConfirmAction);
        
        await AssertSignal(SignalManager.Instance)
            .IsEmitted(SignalManager.SignalName.PerformSelectUnitAction, unit1)
            .WithTimeout(50);
    }
    
    /// <summary>
    /// TODO: Finish test case.
    /// Given the BattleMapCursor receives a PerformConfirmAction signal
    ///     When the game is in the BaseTurnState
    ///     And the position contains no actors
    ///         Then do nothing.
    /// </summary>
    /// <returns></returns>
    [TestCase]
    [RequireGodotRuntime]
    public static async Task TestPerformConfirmActionInBaseTurnStateNoActors()
    {
        ISceneRunner runner = ISceneRunner.Load("res://scenes/test/gdUnit4TestScene_workpad.tscn");
        BattleMapGenerator battleMapGenerator = runner.Scene()!.GetNode<BattleMapGenerator>("%BattleMapGenerator");
        BattleMap testMap = battleMapGenerator.CreateBattleMap("TestMap");
        AssertSignal(SignalManager.Instance).StartMonitoring();
        
        SignalManager.Instance.E(SignalManager.SignalName.PerformConfirmAction);
        
        await AssertSignal(SignalManager.Instance)
            .IsNotEmitted(SignalManager.SignalName.PerformSelectUnitAction)
            .WithTimeout(50);
    }
}