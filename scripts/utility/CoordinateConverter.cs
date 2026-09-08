using Godot;

namespace ClashAndSear.scripts.utility;

public static class CoordinateConverter
{
    private const int TileSize = 16;

    /// <summary>
    /// Determine what tile lies at a pixel location.
    /// </summary>
    /// <param name="pixel">The pixel we're locating.</param>
    /// <returns></returns>
    public static Vector2I FindTileAtPixel(Vector2I pixel)
    {
        return new Vector2I(pixel.X / TileSize, pixel.Y / TileSize);
    }

    /// <summary>
    /// Gets a pixel location of where a tile's at.
    /// </summary>
    /// <param name="tile">The tile we're locating.</param>
    /// <param name="xOffset">An additional x pixel offset.</param>
    /// <param name="yOffset">An additional y pixel offset.</param>
    /// <returns></returns>
    public static Vector2I FindPixelAtTile(Vector2I tile, int xOffset = 0, int yOffset = 0)
    {
        return new Vector2I(tile.X * TileSize + xOffset, tile.Y * TileSize + yOffset);
    }
}