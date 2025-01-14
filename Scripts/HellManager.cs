using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HellManager : MonoBehaviour
{
    public RectTransform image;
    
    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("GamePlay1", LoadSceneMode.Single);
    }
}
