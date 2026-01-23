using Apos.Gui;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;

namespace Tenniz;

public class Collider
{
    public Vector2 Position;
    public Vector2 Size;

    public Collider(Vector2 position, Vector2 size)
    {
        Position = position;
        Size = size;
    }

    public bool Intersects(Collider other)
    {
        Vector2 aMin = Position;
        Vector2 aMax = Position + Size;

        Vector2 bMin = other.Position;
        Vector2 bMax = other.Position + other.Size;

        return IntersectsAabbAabb(aMin, aMax, bMin, bMax);
    }

    public void DebugDraw(SpriteBatch spriteBatch)
    {
#if DEBUG
        spriteBatch.DrawRectangle(new RectangleF(Position, new SizeF(Size.X, Size.Y)), Color.Red);
#endif
    }

    public static bool IntersectsAabbAabb(Vector2 aMin, Vector2 aMax, Vector2 bMin, Vector2 bMax)
    {
        if (aMax.X < bMin.X || aMin.X > bMax.X) return false;
        if (aMax.Y < bMin.Y || aMin.Y > bMax.Y) return false;

        return true;
    }
}
