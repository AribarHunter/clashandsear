using System.Threading.Tasks;
using ClashAndSear.scripts.utility;
using GdUnit4;
using Godot;
using static GdUnit4.Assertions;
// ReSharper disable UnusedMember.Global

namespace ClashAndSear.scripts.ui;

[TestSuite]
public class BattleMapCursorTest
{
    /// <summary>
    /// Given the BattleMapCursor receives a PerformConfirmAction signal
    ///     When the game is in the BaseTurnState
    ///     And the position contains one actor
    ///         Then emit a PerformSelectUnitAction with that actor as the argument.
    /// </summary>
    /// <returns></returns>
    [TestCase]
    [RequireGodotRuntime]
    public static async Task Test_PerformConfirmAction_InBaseTurnState_SingleActor()
    {
        // Arrange ISceneRunner and GameContext
        ISceneRunner runner = ISceneRunner.Load("res://scenes/test/gdUnit4TestScene_workpad.tscn");
        GameContext gameContext = runner.Scene()!.GetNode<GameContext>("%GameContext");
        gameContext.stateMachine.CurrentState = new BaseTurnState();
        AssertSignal(SignalManager.Instance).StartMonitoring();
        // Arrange map
        BattleMapGenerator battleMapGenerator = runner.Scene()!.GetNode<BattleMapGenerator>("%BattleMapGenerator");
        BattleMap testMap = battleMapGenerator.CreateBattleMap("TestMap");
        // Arrange actor
        Actor unit = TestUtilities.ArrangeActor(battleMapGenerator, testMap, new Vector2I(0, 0));
        
        //Act
        SignalManager.Instance.E(SignalManager.SignalName.PerformConfirmAction);
        
        //Assert
        await AssertSignal(SignalManager.Instance)
            .IsEmitted(SignalManager.SignalName.PerformSelectUnitAction, unit)
            .WithTimeout(50);
    }
    
    /// <summary>
    /// Given the BattleMapCursor receives a PerformConfirmAction signal
    ///     When the game is in the BaseTurnState
    ///     And the position contains more than one actor
    ///         Then emit a PerformSelectUnitAction signal with the first actor as the argument.
    /// </summary>
    /// <returns></returns>
    [TestCase]
    [RequireGodotRuntime]
    public static async Task Test_PerformConfirmAction_InBaseTurnState_MultipleActors()
    {
        // Arrange ISceneRunner and GameContext
        ISceneRunner runner = ISceneRunner.Load("res://scenes/test/gdUnit4TestScene_workpad.tscn");
        GameContext gameContext = runner.Scene()!.GetNode<GameContext>("%GameContext");
        gameContext.stateMachine.CurrentState = new BaseTurnState();
        AssertSignal(SignalManager.Instance).StartMonitoring();
        // Arrange map
        BattleMapGenerator battleMapGenerator = runner.Scene()!.GetNode<BattleMapGenerator>("%BattleMapGenerator");
        BattleMap testMap = battleMapGenerator.CreateBattleMap("TestMap");
        // Arrange actors
        Actor unit = TestUtilities.ArrangeActor(battleMapGenerator, testMap, new Vector2I(0, 0));
        TestUtilities.ArrangeActor(battleMapGenerator, testMap, new Vector2I(0, 0));

        //Act
        SignalManager.Instance.E(SignalManager.SignalName.PerformConfirmAction);
        
        //Assert
        await AssertSignal(SignalManager.Instance)
            .IsEmitted(SignalManager.SignalName.PerformSelectUnitAction, unit)
            .WithTimeout(50);
    }
    
    /// <summary>
    /// Given the BattleMapCursor receives a PerformConfirmAction signal
    ///     When the game is in the BaseTurnState
    ///     And the position contains no actors
    ///         Then do nothing.
    /// </summary>
    /// <returns></returns>
    [TestCase]
    [RequireGodotRuntime]
    public static async Task Test_PerformConfirmAction_InBaseTurnState_NoActors()
    {
        // Arrange ISceneRunner and GameContext
        ISceneRunner runner = ISceneRunner.Load("res://scenes/test/gdUnit4TestScene_workpad.tscn");
        GameContext gameContext = runner.Scene()!.GetNode<GameContext>("%GameContext");
        gameContext.stateMachine.CurrentState = new BaseTurnState();
        AssertSignal(SignalManager.Instance).StartMonitoring();
        // Arrange map
        BattleMapGenerator battleMapGenerator = runner.Scene()!.GetNode<BattleMapGenerator>("%BattleMapGenerator");
        battleMapGenerator.CreateBattleMap("TestMap");

        // Act
        SignalManager.Instance.E(SignalManager.SignalName.PerformConfirmAction);
        
        // Assert
        await AssertSignal(SignalManager.Instance)
            .IsNotEmitted(SignalManager.SignalName.PerformSelectUnitAction)
            .WithTimeout(50);
    }
    
    /// <summary>
    /// Given the BattleMapCursor receives a PerformConfirmAction signal
    ///     When the game is in the SelectMoveDestinationState
    ///         Then emit a PerformSelectMoveDestination signal with the location as the argument.
    /// </summary>
    [TestCase]
    [RequireGodotRuntime]
    public static async Task Test_PerformConfirmAction_InSelectMoveDestinationState()
    {
        // Arrange ISceneRunner and GameContext
        ISceneRunner runner = ISceneRunner.Load("res://scenes/test/gdUnit4TestScene_workpad.tscn");
        GameContext gameContext = runner.Scene()!.GetNode<GameContext>("%GameContext");
        gameContext.stateMachine.CurrentState = new statemachine.states.SelectMoveDestinationState();
        AssertSignal(SignalManager.Instance).StartMonitoring();
        // Arrange map
        BattleMapGenerator battleMapGenerator = runner.Scene()!.GetNode<BattleMapGenerator>("%BattleMapGenerator");
        BattleMap testMap = battleMapGenerator.CreateBattleMap("TestMap");
        //Arrange actor
        TestUtilities.ArrangeActorAndSelect(gameContext, battleMapGenerator, testMap, new Vector2I(0, 0));

        //Act
        SignalManager.Instance.E(SignalManager.SignalName.PerformMoveAction, new Vector2I(1, 0));
        SignalManager.Instance.E(SignalManager.SignalName.PerformConfirmAction);
        
        //Assert
        await AssertSignal(SignalManager.Instance)
            .IsEmitted(SignalManager.SignalName.PerformSelectMoveDestination, new Vector2I(1, 0))
            .WithTimeout(50);
    }
}