using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ApplicationManager : MonoBehaviour {
	
	public void Start()
	{
		Debug.Log("HELLO");
	}

	public void Tutorial()
	{
		Debug.Log("Tutorial");
	}

	public void ToIntro()
	{
		SceneManager.LoadScene("Intro1");  
	}

	public void Quit () 
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}
}
