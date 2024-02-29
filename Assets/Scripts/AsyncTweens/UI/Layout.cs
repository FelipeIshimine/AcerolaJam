using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Layout
{
    public Anchor anchor;
    public Pivot pivot;
    public Vector2 position;
}

public static class RectTransformLayoutExtensions
{
    public static void SetLayout(this RectTransform rectTransform, Layout layout)
    {
        rectTransform.SetAnchor(layout.anchor);
        rectTransform.SetPivot(layout.pivot);
        rectTransform.anchoredPosition = layout.position;
    }
}