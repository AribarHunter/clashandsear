using System.Collections.Generic;
using Godot;

namespace ClashAndSear
{
    public partial class BattleMap : TileMapLayer
    {

        public int width;
        public int height;
        public readonly BattleMapTile[,] tiles;
        public BattleMapHighlight battleMapHighlight;
        public AStarGrid2D astarGrid;

        /// <summary>
        /// Convenience array for addition to get the direct neighbors to a tile.
        /// </summary>
        private readonly Vector2I[] _cardinalNeighbors =
        [
            new(0, 1),
            new(0, -1),
            new(1, 0),
            new(-1, 0)
        ];

        public BattleMap(int width, int height, string name, BattleMapHighlight battleMapHighlight)
        {
            // Set up tile stuff.
            this.height = height;
            this.width = width;
            Name = name;
            tiles = new BattleMapTile[width, height];

            // Set up BattleMapHighlight.
            this.battleMapHighlight = battleMapHighlight;
            AddChild(battleMapHighlight);

            // Set up AStarGrid stuff.
            astarGrid = new AStarGrid2D();
            astarGrid.Region = new Rect2I(0, 0, width, height);
            astarGrid.CellSize = new Vector2I(1, 1);
            astarGrid.DefaultComputeHeuristic = AStarGrid2D.Heuristic.Manhattan;
            astarGrid.DefaultEstimateHeuristic = AStarGrid2D.Heuristic.Manhattan;
            astarGrid.DiagonalMode = AStarGrid2D.DiagonalModeEnum.Never;
            astarGrid.Update();

            // Hardcoded TileSet for now.
            Set(TileMapLayer.PropertyName.TileSet, ResourceLoader.Load("resources/tilesets/rtstilemap.tres", TileMapLayer.PropertyName.TileSet));
        }

        public BattleMap()
        {
        }

        /// <summary>
        /// Check if an actor can move from their current position to the indicated one.
        /// </summary>
        /// <param name="actor">The actor we're checking.</param>
        /// <param name="position">The position we're checking.</param>
        /// <returns></returns>
        public bool CanActorMoveToPosition(Actor actor, Vector2I position)
        {
            PathMap possiblePositions =
                Pathfinder.SearchArea(this, actor.battleMapPosition, actor.CanActorMoveBetweenTiles);
            return possiblePositions.ToVector2IList().Contains(position);
        }

        /// <summary>
        /// Checks if there are any Entities recorded on a BattleMapTile found by its position.
        /// </summary>
        /// <param name="tilePosition">The position of the BattleMapTile.</param>
        /// <returns>True if there's an Entity or False if there's not.</returns>
        private bool DoesPositionContainEntity(Vector2I tilePosition)
        {
            return tiles[tilePosition.X, tilePosition.Y].entities.Count > 0;
        }

        /// <summary>
        /// Checks if there are any Actors recorded on a BattleMapTile found by its position.
        /// </summary>
        /// <param name="tilePosition">The position of the BattleMapTile.</param>
        /// <returns>True if there's an Actor or False if there's not.</returns>
        public bool DoesPositionContainActor(Vector2I tilePosition)
        {
            return tiles[tilePosition.X, tilePosition.Y].Actors.Count > 0;
        }

        /// <summary>
        /// Retrieves a list of actors from a position.
        /// </summary>
        /// <param name="position">The BattleMapTile position to be checked.</param>
        /// <returns>A list of all Actors in the tile position.</returns>
        public List<Actor> GetActorsInPosition(Vector2I position)
        {
            return tiles[position.X, position.Y].Actors;
        }

        /// <summary>
        /// Returns the tile at the given position.
        /// </summary>
        /// <param name="position">The BattleMapTile position to be checked.</param>
        /// <returns>Returns the BattleMapTile of a position.</returns>
        public BattleMapTile GetTileAt(Vector2I position)
        {
            return PositionIsInbound(position) ? tiles[position.X, position.Y] : null;
        }

        /// <summary>
        /// Returns the cardinal neighbors BattleMapTiles of a certain position.
        /// </summary>
        /// <param name="position">The position of the BattleMapTile we're checking.</param>
        /// <returns>The neighboring BattleMapTiles. Note this only returns inbound tiles!</returns>
        private List<BattleMapTile> GetCardinalNeighborTilesOf(Vector2I position)
        {
            List<BattleMapTile> result = [];
            for (int i = 0; i < 4; i++)
                result.Add(GetTileAt(position + _cardinalNeighbors[i]));
            return result;
        }

        /// <summary>
        /// Returns the cardinal neighbors BattleMapTiles of a certain position.
        /// </summary>
        /// <param name="tile">The BattleMapTile we're checking.</param>
        /// <returns>The neighboring BattleMapTiles. Note this only returns inbound tiles!</returns>
        public List<BattleMapTile> GetCardinalNeighborTilesOf(BattleMapTile tile)
        {
            return GetCardinalNeighborTilesOf(tile.position);
        }

        /// <summary>
        /// Checks if a position is inside the bounds of this level.
        /// </summary>
        /// <param name="position">The position we're checking.</param>
        /// <returns>True for inbounds, false for outside.</returns>
        public bool PositionIsInbound(Vector2I position)
        {
            return (0 <= position.X && position.X < width &&
                    0 <= position.Y && position.Y < height);
        }
    }
}