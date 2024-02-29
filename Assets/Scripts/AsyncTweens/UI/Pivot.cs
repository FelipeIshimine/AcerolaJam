using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Pivot
{
    TopLeft,
    TopCenter,
    TopRight,
    MiddleLeft,
    MiddleCenter,
    MiddleRight,
    BottomLeft,
    BottomCenter,
    BottomRight
}

public static class PivotExtensions
{
    public static Vector2 ToVector2(this Pivot pivot)
    {
        switch (pivot)
        {
            case Pivot.TopLeft:
                return new Vector2(0, 1);
            case Pivot.TopCenter:
                return new Vector2(0.5f, 1);
            case Pivot.TopRight:
                return new Vector2(1, 1);
            case Pivot.MiddleLeft:
                return new Vector2(0, 0.5f);
            case Pivot.MiddleCenter:
                return new Vector2(0.5f, 0.5f);
            case Pivot.MiddleRight:
                return new Vector2(1, 0.5f);
            case Pivot.BottomLeft:
                return new Vector2(0, 0);
            case Pivot.BottomCenter:
                return new Vector2(0.5f, 0);
            default: // Pivot.BottomRight
                return new Vector2(1, 0);
        }
    }
}

public static class RectTransformPivotExtensions
{
    public static void SetPivot(this RectTransform rectTransform, Pivot pivot)
    {
        rectTransform.pivot = pivot.ToVector2();
    }
}
