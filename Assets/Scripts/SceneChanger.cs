using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
	private string currScene;
	public string prevScene;

	void Start()
	{
		DontDestroyOnLoad(this.gameObject);  //Allow this object to persist between scene changes
		prevScene = "Main Menu";
		currScene = "Main Menu";
		SceneManager.LoadScene("Main Menu");
	}

	public void ChangeScene(string sceneName)
	{
		prevScene = currScene;
		currScene = sceneName;
		SceneManager.LoadScene(sceneName);
	}

	public void PrevScene()
	{
		string temp = currScene;
		currScene = prevScene;
		prevScene = temp;
		SceneManager.LoadScene(currScene);
	}
}
