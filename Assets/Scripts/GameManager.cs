using UnityEngine;
using System.Collections;

/// <summary>
/// Taken from the Unity Roguelike tutorial.
/// </summary>
/// <see cref="https://unity3d.com/learn/tutorials/projects/2d-roguelike-tutorial/writing-game-manager?playlist=17150"/>>
public class GameManager : MonoBehaviour {

	public BoardManager boardScript;

	private int level = 3;
	
	// Use this for initialization
	void Awake () {
		boardScript = GetComponent<BoardManager> ();
		InitGame ();
	}

	void InitGame () {
		boardScript.SetupScene (level);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
