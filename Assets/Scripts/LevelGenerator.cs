using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour {

	public static int numRooms = 20;
	int currNumRooms = 0;

	public GameObject[,] rooms = new GameObject[numRooms * 2 + 1, numRooms * 2 + 1];

	public GameObject[] SpawnRooms;

	public int numPuzzleRooms = 2;
	public GameObject[] PuzzleRooms;

	public int numObstacleRooms = 2;
	public GameObject[] ObstacleRooms;

	public int numBattleRooms = 2;
	public GameObject[] BattleRooms;

	public int numRestRooms = 2;
	public GameObject[] RestRooms;
	
	public GameObject[] BossRooms;

	public int roomWidth = 27;
	public int roomHeight = 17;

	public class Vector2I {
		public int x;
		public int y;
		public Vector2I(int x, int y) {
			this.x = x;
			this.y = y;
		}
	}

	public enum Direction {
		UP,
		DOWN,
		LEFT,
		RIGHT
	}

	public HashSet<Vector2I> availablePositions = new HashSet<Vector2I> ();
	public HashSet<Vector2I> usedPositions = new HashSet<Vector2I> ();

	// Use this for initialization
	void Start () {
		Generate ();
	}

	void Generate() {
		int currNumPuzzleRooms = 0;
		int currNumObstacleRooms = 0;
		int currNumBattleRooms = 0;
		int currNumRestRooms = 0;

		rooms [numRooms / 2, numRooms / 2] = RandomRoom (SpawnRooms);
		AddAvailableRooms (numRooms / 2, numRooms / 2);

		while (currNumRooms < numRooms) {
			AddRooms(RandomDirection (), Random.Range (1, numRooms - currNumRooms);
		}

		InstantiateRooms ();
	}

	//adds adjacent positions as potential room locations
	void AddAvailableRooms(int x, int y) {
		usedPositions.Add (new Vector2I (x, y));
		if (rooms [x - 1, y] == null) {
			availablePositions.Add(new Vector2I(x-1, y));
		}
		if (rooms [x + 1, y] == null) {
			availablePositions.Add (new Vector2I(x+1, y));
		}
		if (rooms [x, y - 1] == null) {
			availablePositions.Add (new Vector2I(x, y-1));
		}
		if (rooms [x, y + 1] == null) {
			availablePositions.Add (new Vector2I(x, y+1));
		}
	}

	void AddRooms(Direction direction, int length) {
		Vector2I[] availablePositionArray = new Vector2I[availablePositions.Count];
		availablePositions.CopyTo (availablePositionArray);
		Vector2I position = availablePositionArray [Random.Range (0, availablePositionArray.Length)];
		availablePositions.Remove (position);
		rooms[position.x, position.y] = RandomRoom (SpawnRooms);
		AddAvailableRooms (position.x, position.y);
	}

	void InstantiateRooms() {
		for (int i=0; i<rooms.GetLength (0); i++) {
			for (int j=0; j<rooms.GetLength (1); j++) {
				if (rooms[i, j] != null) {
					GameObject room = Instantiate (rooms[i, j]);
					Vector3 position = new Vector3((i-numRooms/2)*roomWidth, (j-numRooms/2)*roomHeight, 0);
					room.transform.position = position;
				}
			}
		}
	}

	GameObject RandomRoom(GameObject[] rooms) {
		return rooms[Random.Range (0, rooms.Length)];
	}

	Direction RandomDirection() {
		return (Direction) Random.Range (0, 3);
	}
}
