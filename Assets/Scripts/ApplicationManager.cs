﻿using UnityEngine;
using System.Collections;

public class ApplicationManager : MonoBehaviour {
	
	public void Start()
	{
		Debug.Log("HELLO");
	}

	public void Tutorial()
	{
		Debug.Log("Tutorial");
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