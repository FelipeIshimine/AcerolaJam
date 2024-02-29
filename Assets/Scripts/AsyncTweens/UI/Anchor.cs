using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Anchor
{
    TopLeft,
    TopCenter,
    TopRight,
    TopStretch,
    MiddleLeft,
    MiddleCenter,
    MiddleRight,
    MiddleStretch,
    BottomLeft,
    BottomCenter,
    BottomRight,
    BottomStretch,
    StretchLeft,
    StretchCenter,
    StretchRight,
    StretchStretch
}

public static class AnchorExtensions
{
    public static Vector2 Min(this Anchor anchor)
    {
        switch (anchor)
        {
            case Anchor.TopLeft:
                return new Vector2(0, 1);
            case Anchor.TopCenter:
                return new Vector2(0.5f, 1);
            case Anchor.TopRight:
                return new Vector2(1, 1);
            case Anchor.TopStretch:
                return new Vector2(0, 1);
            case Anchor.MiddleLeft:
                return new Vector2(0, 0.5f);
            case Anchor.MiddleCenter:
                return new Vector2(0.5f, 0.5f);
            case Anchor.MiddleRight:
                return new Vector2(1, 0.5f);
            case Anchor.MiddleStretch:
                return new Vector2(0, 0.5f);
            case Anchor.BottomLeft:
                return new Vector2(0, 0);
            case Anchor.BottomCenter:
                return new Vector2(0.5f, 0);
            case Anchor.BottomRight:
                return new Vector2(1, 0);
            case Anchor.BottomStretch:
                return new Vector2(0, 0);
            case Anchor.StretchLeft:
                return new Vector2(0, 0);
            case Anchor.StretchCenter:
                return new Vector2(0.5f, 0);
            case Anchor.StretchRight:
                return new Vector2(1, 0);
            default:// Anchor.StretchStretch:
                return new Vector2(0, 0);
        }
    }

    public static Vector2 Max(this Anchor anchor)
    {
        switch (anchor)
        {
            case Anchor.TopStretch:
                return new Vector2(1, 1);
            case Anchor.MiddleStretch:
                return new Vector2(1, 0.5f);
            case Anchor.BottomStretch:
                return new Vector2(1, 0);
            case Anchor.StretchLeft:
                return new Vector2(0, 1);
            case Anchor.StretchCenter:
                return new Vector2(0.5f, 1);
            case Anchor.StretchRight:
                return new Vector2(1, 1);
            case Anchor.StretchStretch:
                return new Vector2(1, 1);
            default:
                return Min(anchor);
        }
    }
}

public static class RectTransformAnchorExtensions
{
    public static void SetAnchor(this RectTransform rectTransform, Anchor anchor)
    {
        rectTransform.anchorMin = anchor.Min();
        rectTransform.anchorMax = anchor.Max();
    }
}
