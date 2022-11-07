using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private GameObject sceneManager;

    public void Start()
    {
        sceneManager = GameObject.FindWithTag("SceneManager");
    }
    public void PlayGame()
    {
        ChangeToScene("Game");
    }
    public void Controls()
    {
        ChangeToScene("Controls");
    }

    public void Back()
    {
        sceneManager.GetComponent<SceneChanger>().PrevScene();
    }

    private void ChangeToScene(string scene)
    {
        sceneManager.GetComponent<SceneChanger>().ChangeScene(scene);
    }
}
