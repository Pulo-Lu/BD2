using System.Collections.Generic;
using UnityEngine;

public static class GameTools
{
    public static void DestroyChildren(this Transform t)
    {
        bool isPlaying = Application.isPlaying;
        while (t.childCount != 0)
        {
            Transform child = t.GetChild(0);
            if (isPlaying)
            {
                child.SetParent(null);
                UnityEngine.Object.Destroy(child.gameObject);
            }
            else
            {
                UnityEngine.Object.DestroyImmediate(child.gameObject);
            }
        }
    }

    public static void SetParentNormal(this GameObject child, Transform parent)
    {
        SetParentNormalInternal(child.transform, parent);
    }

    public static void SetParentNormal(this Transform child, GameObject parent)
    {
        SetParentNormalInternal(child, parent.transform);
    }

    public static void SetParentNormal(this GameObject child, GameObject parent)
    {
        SetParentNormalInternal(child.transform, parent.transform);
    }

    public static void SetParentNormal(this Transform child, Transform parent)
    {
        SetParentNormalInternal(child, parent);
    }

    private static void SetParentNormalInternal(Transform child, Transform parent)
    {
        child.SetParent(parent, worldPositionStays: false);
        child.localPosition = Vector3.zero;
        child.localScale = Vector3.one;
        child.localEulerAngles = Vector3.zero;
		
        if (child is RectTransform rectTransform)
        {
            rectTransform.anchoredPosition = Vector2.zero;
        }
    }
}