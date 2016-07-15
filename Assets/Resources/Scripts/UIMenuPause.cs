using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIMenuPause : MonoBehaviour {

	public GameObject PausePanel;
	// public GameObject MainPanel;

	private bool isPaused = false;

	//////////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////

	void Start () {
		PausePanel.SetActive(false);
	}

	void Update () {
		if (Input.GetButtonDown("Pause")) {
			isPaused = !isPaused;
		}

		if (isPaused) {
			PausePanel.SetActive(true);
			Time.timeScale = 0;
		}

		if (!isPaused) {
			PausePanel.SetActive(false);
			Time.timeScale = 1;
		}
	}

	//////////////////////////////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////////////////////////////////////////////

	public void Resume () {
		isPaused = false;
	}

	public void Restart () {
		Scene currentScene = SceneManager.GetActiveScene ();
		string currentSceneName = currentScene.name;
		Debug.Log(string.Format("{0} Restart: {1}", GetType(), currentSceneName));

		SceneManager.LoadScene(currentSceneName);
	}

	public void MainMenu () {
		SceneManager.LoadScene("MainMenu");

//		PausePanel.SetActive(false);
		//you sure you want to quit to the mani menu?
//		MainPanel.SetActive(false);
	}

	public void Quit () {
		Application.Quit();
	}

}
