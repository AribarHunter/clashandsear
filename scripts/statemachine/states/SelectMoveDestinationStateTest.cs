using System.Threading.Tasks;
using GdUnit4;
using static GdUnit4.Assertions;
// ReSharper disable UnusedMember.Global

namespace ClashAndSear.scripts.statemachine.states
{
    [TestSuite]
    public class SelectMoveDestinationStateTest
    {
        /// <summary>
        /// Validates pressing Cancel moves to the BaseTurnState.
        /// </summary>
        [TestCase]
        [RequireGodotRuntime]
        public static async Task PressCancelButton()
        {
            ISceneRunner runner = ISceneRunner.Load("res://scenes/test/gdUnit4TestScene_workpad.tscn");
            AssertThat(runner).IsNotNull();
            AssertThat(runner.Scene()).IsNotNull();
         
            GameContext gameContext = runner.Scene()!.GetNode<GameContext>("%GameContext");
            gameContext.stateMachine.CurrentState = new SelectMoveDestinationState();
         
            runner.SimulateActionPress("cancel");
            await runner.AwaitInputProcessed();

            AssertThat(GameContext.Instance.stateMachine.CurrentState)
                .IsInstanceOf<BaseTurnState>();
        }
    }
}