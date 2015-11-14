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

	int direction = 0;

	public enum Direction {
		UP,
		DOWN,
		LEFT,
		RIGHT
	}

	public HashSet<Vector2I> usedPositions = new HashSet<Vector2I> ();
	public HashSet<Vector2I> availablePositions = new HashSet<Vector2I> ();

	// Use this for initialization
	void Start () {
		Generate ();
	}

	void Generate() {
		int currNumPuzzleRooms = 0;
		int currNumObstacleRooms = 0;
		int currNumBattleRooms = 0;
		int currNumRestRooms = 0;

		Vector2I center = new Vector2I (numRooms / 2, numRooms / 2);
		SetRoom (center, RandomRoom (SpawnRooms));
		AddAvailableRooms (center);
		currNumRooms++;

		while (currNumRooms < numRooms) {
			AddRooms(RandomDirection (), Random.Range (1, (int) Mathf.Sqrt(numRooms)));
		}

		InstantiateRooms ();
	}

	void AddRooms(Direction direction, int length) {
		Vector2I position = RandomUsedPosition ();
		int addedRooms = 0;
		while (addedRooms < length) {
			position = GetNextPosition (direction, position);
			if (!RoomInRange (position)) {
				break;
			} else if (GetRoom (position) == null) {
				SetRoom (position, RandomRoom (SpawnRooms));
				AddAvailableRooms (position);
				currNumRooms++;
				addedRooms++;
			}
//			} else {
//				break;
//			}
		}
	}
	
	//adds adjacent positions as potential room locations
	void AddAvailableRooms(Vector2I position) {
		usedPositions.Add (position);
		Vector2I up = GetNextPosition (Direction.UP, position);
		if (RoomInRange (up) && GetRoom (up) == null) {
			availablePositions.Add(up);
		}
		Vector2I down = GetNextPosition (Direction.DOWN, position);
		if (RoomInRange (down) && GetRoom (down) == null) {
			availablePositions.Add (down);
		}
		Vector2I left = GetNextPosition (Direction.LEFT, position);
		if (RoomInRange (left) && GetRoom (left) == null) {
			availablePositions.Add (left);
		}
		Vector2I right = GetNextPosition (Direction.RIGHT, position);
		if (RoomInRange (right) && GetRoom (right) == null) {
			availablePositions.Add (right);
		}
	}

	Vector2I GetNextPosition(Direction direction, Vector2I position) {
		switch (direction) {
		case Direction.UP:
			return new Vector2I(position.x, position.y+1);
		case Direction.DOWN:
			return new Vector2I(position.x, position.y-1);
		case Direction.LEFT:
			return new Vector2I(position.x-1, position.y);
		case Direction.RIGHT:
			return new Vector2I(position.x+1, position.y);
		}
		return null;
	}

	GameObject GetRoom(Vector2I position) {
		return rooms [position.x, position.y];
	}

	void SetRoom(Vector2I position, GameObject room) {
		rooms [position.x, position.y] = room;
	}
	
	bool RoomInRange(Vector2I position) {
		return position.x >= 0 && position.x < rooms.GetLength (0) &&
			position.y >= 0 && position.y < rooms.GetLength (1);
	}

	Vector2I RandomUsedPosition() {
		Vector2I[] usedPositionArray = new Vector2I[usedPositions.Count];
		usedPositions.CopyTo (usedPositionArray);
		return usedPositionArray [Random.Range (0, usedPositionArray.Length)];
	}

	Vector2I RandomAvailablePosition() {
		Vector2I[] availablePositionArray = new Vector2I[availablePositions.Count];
		availablePositions.CopyTo (availablePositionArray);
		return availablePositionArray [Random.Range (0, availablePositionArray.Length)];
	}

	GameObject RandomRoom(GameObject[] rooms) {
		return rooms[Random.Range (0, rooms.Length)];
	}

	Direction RandomDirection() {
		direction++;
		return (Direction) (direction % 4);
	}

	void InstantiateRooms() {
		for (int i=0; i<rooms.GetLength (0); i++) {
			for (int j=0; j<rooms.GetLength (1); j++) {
				if (rooms[i, j] != null) {
					GameObject room = Instantiate (rooms[i, j]);
					Vector3 position = new Vector3((i-numRooms/2)*(roomWidth-1), (j-numRooms/2)*(roomHeight-1), 0);
					room.transform.position = position;
				}
			}
		}
	}
}
