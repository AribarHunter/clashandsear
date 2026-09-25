using System.Threading.Tasks;
using ClashAndSear.scripts.utility;
using GdUnit4;
using Godot;
using static GdUnit4.Assertions;
// ReSharper disable UnusedMember.Global

namespace ClashAndSear.scripts.statemachine.states
{
    [TestSuite]
    public class SelectMoveDestinationStateTest
    {
        /// <summary>
        /// Given the game is in SelectMoveDestinationState
        ///     When the game receives a Cancel input
        ///         Then the game returns to the BaseTurnState.
        /// </summary>
        [TestCase]
        [RequireGodotRuntime]
        public static async Task Test_PressCancelButton()
        {
            // Arrange ISceneRunner and GameContext
            ISceneRunner runner = ISceneRunner.Load("res://scenes/test/gdUnit4TestScene_workpad.tscn");
            GameContext gameContext = runner.Scene()!.GetNode<GameContext>("%GameContext");
            gameContext.stateMachine.CurrentState = new SelectMoveDestinationState();
            // Arrange map
            BattleMapGenerator battleMapGenerator = runner.Scene()!.GetNode<BattleMapGenerator>("%BattleMapGenerator");
            BattleMap testMap = battleMapGenerator.CreateBattleMap("TestMap");
            //Arrange actor
            TestUtilities.ArrangeActorAndSelect(gameContext, battleMapGenerator, testMap, new Vector2I(0, 0));
         
            // Act
            runner.SimulateActionPress("cancel");
            await runner.AwaitInputProcessed();

            // Assert
            AssertThat(GameContext.Instance.stateMachine.CurrentState)
                .IsInstanceOf<BaseTurnState>();
            AssertThat(GameContext.Instance.selectedActor == null);
        }

        /// <summary>
        /// Given the game is in SelectMoveDestinationState
        ///     When the game receives a PerformSelectMoveDestination signal
        ///     And it is a valid move destination
        ///         Then update selectedPosition
        ///         And remove UI highlights
        ///         And move onto the UnitMovingState.
        /// </summary>
        [TestCase]
        [RequireGodotRuntime]
        public static async Task Test_PerformSelectMoveDestinationAction_ValidDestination()
        {
            // Arrange ISceneRunner and GameContext
            ISceneRunner runner = ISceneRunner.Load("res://scenes/test/gdUnit4TestScene_workpad.tscn");
            GameContext gameContext = runner.Scene()!.GetNode<GameContext>("%GameContext");
            gameContext.stateMachine.CurrentState = new SelectMoveDestinationState();
            // Arrange map
            BattleMapGenerator battleMapGenerator = runner.Scene()!.GetNode<BattleMapGenerator>("%BattleMapGenerator");
            BattleMap testMap = battleMapGenerator.CreateBattleMap("TestMap");
            gameContext.battleMap = testMap;
            //Arrange actor
            TestUtilities.ArrangeActorAndSelect(gameContext, battleMapGenerator, testMap, new Vector2I(0, 0));
            //Arrange cursor
            SignalManager.Instance.E(SignalManager.SignalName.PerformMoveAction, Vector2.Right);
            
            // Act
            runner.SimulateActionPress("confirm");
            await runner.AwaitInputProcessed();

            // Assert
            AssertThat(GameContext.Instance.selectedPosition)
                .IsEqual(new Vector2I(1, 0));
            AssertThat(!testMap.battleMapHighlight.IsUsed);
            AssertThat(GameContext.Instance.stateMachine.CurrentState)
                .IsInstanceOf<UnitMovingState>();
        }
        
        /// <summary>
        /// Given the game is in SelectMoveDestinationState
        ///     When the game receives a PerformSelectMoveDestination signal
        ///     And it is an invalid move destination
        ///         Then do not adjust selectedPosition
        ///         And do not remove UI highlights
        ///         And stay in SelectMoveDestinationState.
        ///     
        /// </summary>
        [TestCase]
        [RequireGodotRuntime]
        public static async Task Test_PerformSelectMoveDestinationAction_InvalidDestination()
        {
            // Arrange ISceneRunner and GameContext
            ISceneRunner runner = ISceneRunner.Load("res://scenes/test/gdUnit4TestScene_workpad.tscn");
            GameContext gameContext = runner.Scene()!.GetNode<GameContext>("%GameContext");
            gameContext.stateMachine.CurrentState = new SelectMoveDestinationState();
            // Arrange map
            BattleMapGenerator battleMapGenerator = runner.Scene()!.GetNode<BattleMapGenerator>("%BattleMapGenerator");
            BattleMap testMap = battleMapGenerator.CreateBattleMap("TestMap");
            //Arrange actor
            TestUtilities.ArrangeActorAndSelect(gameContext, battleMapGenerator, testMap, new Vector2I(0, 0));
            // GameContext.Instance.selectedPosition = new Vector2I(5, 5);
            //Arrange cursor
            SignalManager.Instance.E(SignalManager.SignalName.PerformMoveAction, new Vector2I(5,0));

            // Act
            runner.SimulateActionPress("confirm");
            await runner.AwaitInputProcessed();

            // Assert
            AssertThat(GameContext.Instance.selectedPosition)
                .IsEqual(new Vector2I(0, 0));
            AssertThat(testMap.battleMapHighlight.IsUsed);
            AssertThat(GameContext.Instance.stateMachine.CurrentState)
                .IsInstanceOf<SelectMoveDestinationState>();
            
;        }
    }
}