using ClashAndSear.scripts.utility;
using Godot;

namespace ClashAndSear.scripts.entity
{
    public partial class Entity : Node2D
    {
        public Vector2I battleMapPosition;
  
        /// <summary>
        /// Sets an internal position variable and updates the node's graphical position.
        /// </summary>
        /// <param name="position">The battle map position to be set.</param>
        public void UpdateBattleMapPosition(Vector2I position)
        {
            battleMapPosition = position;
            Position = CoordinateConverter.FindPixelAtTile(battleMapPosition);
        }
    }
}