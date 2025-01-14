using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public RectTransform image;
    public void AlterPosition()
    {
        image.anchoredPosition = new Vector2(Random.Range(0, 500), Random.Range(0, 500));
    }
}
