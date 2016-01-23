using System.Collections.Generic;
using UnityEngine;

public enum Side
{
    Left,
    Right,
    Top,
    Bottom,
    BottomRight,
    BottomLeft,
    TopRight,
    TopLeft,
    None,
}

public class SideMath
{
    public static Vector2 Side2Direction(Side s)
    {
        Vector2 direction = new Vector2();

        switch (s)
        {
            case Side.Left:
                direction = new Vector2(-1, 0);
                break;
            case Side.Right:
                direction = new Vector2(1, 0);
                break;
            case Side.Top:
                direction = new Vector2(0, 1);
                break;
            case Side.Bottom:
                direction = new Vector2(0, -1);
                break;
            case Side.BottomRight:
                direction = new Vector2(1, 0);
                break;
            case Side.BottomLeft:
                direction = new Vector2(-1, 0);
                break;
            case Side.TopRight:
                direction = new Vector2(1, 0);
                break;
            case Side.TopLeft:
                direction = new Vector2(-1, 0);
                break;
            case Side.None:
                direction = new Vector2(0, 0);
                break;
            default:
                break;
        }

        return direction;
    }

    public static List<Side> GetCompatibleSides(Side side)
    {
        List<Side> sides = new List<Side>();

        switch (side)
        {
            case Side.Left:
                sides.Add(Side.Right);
                sides.Add(Side.BottomRight);
                sides.Add(Side.TopRight);
                break;
            case Side.Right:
                sides.Add(Side.Left);
                sides.Add(Side.BottomLeft);
                sides.Add(Side.TopLeft);
                break;
            case Side.Top:
                sides.Add(Side.Bottom);
                break;
            case Side.Bottom:
                sides.Add(Side.Top);
                break;
            case Side.BottomRight:
                sides.Add(Side.Left);
                sides.Add(Side.TopLeft);
                break;
            case Side.BottomLeft:
                sides.Add(Side.Right);
                sides.Add(Side.TopRight);
                break;
            case Side.TopRight:
                sides.Add(Side.Left);
                sides.Add(Side.BottomLeft);
                break;
            case Side.TopLeft:
                sides.Add(Side.Right);
                sides.Add(Side.BottomRight);
                break;
            case Side.None:
                sides.Add(Side.Right);
                sides.Add(Side.BottomRight);
                sides.Add(Side.TopRight);
                sides.Add(Side.Left);
                sides.Add(Side.BottomLeft);
                sides.Add(Side.TopLeft);
                sides.Add(Side.Top);
                break;
            default:
                break;
        }

        return sides;
    }
}