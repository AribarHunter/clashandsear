using System.Threading.Tasks;
using GdUnit4;
using Godot;
using static GdUnit4.Assertions;
// ReSharper disable CheckNamespace
// ReSharper disable UnusedMember.Global

namespace ClashAndSear
{
    [TestSuite]
    public class BaseTurnStateTest
    {
        /// <summary>
        /// When BaseTurnState is entered...
        ///     Then the PerformSelectUnitAction signal is connected
        ///     And a PerformHighlightIfHoveringOverActorAction signal is emitted. 
        /// </summary>
        [TestCase]
        [RequireGodotRuntime]
        public static async Task TestEnterState()
        {
            ISceneRunner runner = ISceneRunner.Load("res://scenes/test/gdUnit4TestScene_workpad.tscn");
            GameContext gameContext = runner.Scene()!.GetNode<GameContext>("%GameContext");
            AssertSignal(SignalManager.Instance).StartMonitoring();
            AssertBool(SignalManager.Instance.IsConnected(
                    SignalManager.SignalName.PerformSelectUnitAction,
                    new Callable(gameContext.stateMachine.CurrentState,"PerformSelectUnitAction")))
                .IsFalse();
            
            gameContext.stateMachine.CurrentState = new BaseTurnState();

            AssertBool(SignalManager.Instance.IsConnected(
                SignalManager.SignalName.PerformSelectUnitAction,
                new Callable(gameContext.stateMachine.CurrentState,"PerformSelectUnitAction")))
                .IsTrue();
            await AssertSignal(SignalManager.Instance)
                .IsEmitted(SignalManager.SignalName.PerformHighlightIfHoveringOverActorAction)
                .WithTimeout(50);
        }
    }
}