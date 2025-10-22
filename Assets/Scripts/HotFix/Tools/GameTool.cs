using System.Collections.Generic;
using UnityEngine;

public static class GameTools
{
    #region 物体相关

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

    #endregion

    #region 数据相关

    /// <summary>
    ///     获取一个随机整数，范围为 [min, max] 包含 min 和 max
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static int RandomInt(int min, int max) {
        return Random.Range(min, max + 1);
    }

    /// <summary>
    ///     获取一个随机布尔值，概率为 0.5 为true，0.5 为false
    /// </summary>
    /// <returns></returns>
    public static bool RandomBool() {
        return Random.value < 0.5f;
    }

    /// <summary>
    ///     获取一个随机布尔值，概率为 probability 为true，(1-probability) 为false
    /// </summary>
    /// <param name="probability"></param>
    /// <returns></returns>
    public static bool RandomBool(float probability) {
        return Random.value < probability;
    }

    public static bool RandomBool(int probability) {
        return RandomBool(probability / 100f);
    }

    public static float RandomFloat(float min, float max) {
        return Random.Range(min, max);
    }

    /// <summary>
    /// 从100%中获取概率
    /// </summary>
    /// <param name="f"></param>
    /// <returns> true 命中概率，false 未命中概率 </returns>
    public static bool Probability(float f) {

        float randomValue = Random.Range(0f, 100f);
        if (randomValue < f)
        {
            return true;
        }
        else {
            return false;
        }
    }

    #endregion
   
}