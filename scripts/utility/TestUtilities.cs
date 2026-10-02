using ClashAndSear.scripts.battlemap;
using Godot;

namespace ClashAndSear.scripts.utility;

public static class TestUtilities
{
    /// <summary>
    /// Utility method to add an Actor to the test scene at a certain position.
    /// </summary>
    /// <param name="battleMapGenerator">Reference to the BattleMapGenerator.</param>
    /// <param name="battleMap">Place the Actor on this BattleMap.</param>
    /// <param name="position">Place the Actor at this position.</param>
    /// <returns></returns>
    public static entity.Actor ArrangeActor(BattleMapGenerator battleMapGenerator, BattleMap battleMap, Vector2I position)
    {
        PackedScene unitPackedScene = GD.Load<PackedScene>("res://scenes/entities/actor.tscn");
        entity.Actor unit = unitPackedScene.Instantiate<entity.Actor>();
        battleMapGenerator.AddEntityToBattleMapAtPosition(unit, battleMap, position);
        return unit;
    }

    /// <summary>
    /// Utility method to add an Actor to the test scene at a certain position. Also selects them in GameContext.
    /// </summary>
    /// <param name="gameContext">Reference to the GameContext.</param>
    /// <param name="battleMapGenerator">Reference to the BattleMapGenerator.</param>
    /// <param name="battleMap">Place the Actor on this BattleMap.</param>
    /// <param name="position">Place the Actor at this position.</param>
    /// <returns></returns>
    public static void ArrangeActorAndSelect(GameContext gameContext, BattleMapGenerator battleMapGenerator,
        BattleMap battleMap, Vector2I position)
    {
        PackedScene unitPackedScene = GD.Load<PackedScene>("res://scenes/entities/actor.tscn");
        entity.Actor unit = unitPackedScene.Instantiate<entity.Actor>();
        battleMapGenerator.AddEntityToBattleMapAtPosition(unit, battleMap, position);
        gameContext.selectedActor = unit;
    }
}