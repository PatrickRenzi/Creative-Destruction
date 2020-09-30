using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenScene : MonoBehaviour
{
    public void LoadScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("AsteriodBelt");
    }

    public void LoadScene(int mySceneIndex)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mySceneIndex);
    }
}