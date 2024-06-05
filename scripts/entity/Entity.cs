using Godot;
using System;

public partial class Entity : Sprite2D
{

    public Vector2I currentPosition;
    public Entity(string name, Texture2D texture2D)
    {
        Name = name;
        Texture = texture2D;
        Set(PropertyName.Texture, texture2D);
        Centered = false;
    }

    public void UpdateTransformToTile()
    {
        Position = CoordinateConverter.FindPixelAtTile(currentPosition, 0, 0);
    }
}