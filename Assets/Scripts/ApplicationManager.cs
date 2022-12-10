﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.SceneManagement;

public class ApplicationManager : MonoBehaviour {
	
	public void StartGame()
	{
		Debug.Log("Go to intro");
		SceneManager.LoadScene("Intro1");  
	}

	public void Tutorial()
	{
		Debug.Log("Tutorial");
	}

	public void BackToMenu() 
	{
		SceneManager.LoadScene("Menu");
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
