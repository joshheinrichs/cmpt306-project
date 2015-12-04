using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Procedurally generates a level from the given sets of rooms to the specified size.
 */
public class LevelGenerator : MonoBehaviour {

	/**
	 * Basically an index pair for the room and door arrays, makes the code a bit cleaner.
	 */ 
	public class Vector2I {
		public int x, y;

		public Vector2I(int x, int y) {
			this.x = x;
			this.y = y;
		}

		public int ManhattanDistance(Vector2I v) {
			return Mathf.Abs (this.x - v.x) + Mathf.Abs (this.y - v.y);
		}
	}

	public int numRooms = 10;
	int currNumRooms = 0;

	public GameObject[,] rooms;
	public Doors[,] doors;
	
	public GameObject[] SpawnRooms;
	/** Probability that a puzzle room is generated. */
	public float pPuzzleRoom = 0.0f;
	public GameObject[] PuzzleRooms;
	/** Probability that an obstacle room is generated. */
	public float pObstacleRoom = 0.5f;
	public GameObject[] ObstacleRooms;
	/** Probability that a battle room is generated. */
	public float pBattleRoom = 0.3f;
	public GameObject[] BattleRooms;
	/** Probability that a rest room is generated. */
	public float pRestRoom = 0.2f;
	public GameObject[] RestRooms;
	public GameObject[] BossRooms;

	public GameObject door;
	public GameObject wall;

	public int roomWidth = 27;
	public int roomHeight = 17;

	Queue<GameObject> queueRooms = new Queue<GameObject>();
	Queue<GameObject> spawroom = new Queue<GameObject>();
	Queue<GameObject> bossroom = new Queue<GameObject>();
	Queue<GameObject> puzroom = new Queue<GameObject>();
	Queue<GameObject> obsroom = new Queue<GameObject>();
	Queue<GameObject> batroom = new Queue<GameObject>();
	Queue<GameObject> resroom = new Queue<GameObject>();

	int direction = 0;
	
	public HashSet<Vector2I> usedPositions = new HashSet<Vector2I> ();
	public HashSet<Vector2I> availablePositions = new HashSet<Vector2I> ();

	// Use this for initialization
	void Start () {
		rooms = new GameObject[numRooms * 2 + 1, numRooms * 2 + 1];
		doors = new Doors[numRooms * 2 + 1, numRooms * 2 + 1];
		Random.seed = System.Environment.TickCount;
		enqueueRooms();
        Generate ();
	}

	/**
	 * Randomly generates a set of connected rooms.
	 */
	void Generate() {
		Vector2I center = new Vector2I (numRooms / 2, numRooms / 2);
		GameObject spawnRoom = spawroom.Dequeue();
		AddRoom (center, spawnRoom, null);

		while (currNumRooms < numRooms) {
			AddRooms(RandomDirection (), Random.Range (1, (int) Mathf.Sqrt(numRooms)));
		}

		Vector2I[] positions = new Vector2I[availablePositions.Count];
		availablePositions.CopyTo (positions);

		Vector2I furthestPosition = positions [0];
		for (int i=1; i<positions.Length; i++) {
			if (positions[i].ManhattanDistance(center) > furthestPosition.ManhattanDistance(center)) {
				furthestPosition = positions[i];
			}
		}
		Direction direction = DirectionMethods.OppositeDirection(AdjacentRoomDirection (furthestPosition));

		GameObject bossRoom = bossroom.Dequeue();
		AddRoom (furthestPosition, bossRoom, direction);

		InstantiateRooms ();
	}
	Queue<GameObject> makeQueueRooms(GameObject[] rooms)
	{
		if (rooms.Length == 1)
			Debug.Log("This is the spawnroom");
		Queue<GameObject> queueduprooms = new Queue<GameObject>();
		List<GameObject> roomlist = new List<GameObject>();
		foreach (GameObject i in rooms)
		{
			roomlist.Add(i);
		}
		int randint;
		while (roomlist.Count != 0)
		{
			randint = Random.Range(0, roomlist.Count);
			Debug.Log("randint: " + randint);
			queueduprooms.Enqueue(roomlist[randint]);
			roomlist.RemoveAt(randint);
		}
		return queueduprooms;
    }

	/** Create a queue of rooms */
	void enqueueRooms() {
		bossroom = makeQueueRooms(BossRooms);
		puzroom = makeQueueRooms(PuzzleRooms);
		obsroom = makeQueueRooms(ObstacleRooms);
		batroom = makeQueueRooms(BattleRooms);
		resroom = makeQueueRooms(RestRooms);
		spawroom = makeQueueRooms(SpawnRooms);
		float rand;
		for (int i = 0; i < numRooms; i++) { 
			rand = Random.Range(0f, 1f);
			if (rand < pPuzzleRoom && puzroom.Count !=0)
			{
				queueRooms.Enqueue(puzroom.Dequeue());
			}
			else if (rand < pPuzzleRoom + pObstacleRoom && obsroom.Count != 0)
			{
				queueRooms.Enqueue(obsroom.Dequeue());
			}
			else if (rand < pPuzzleRoom + pObstacleRoom + pBattleRoom && batroom.Count != 0)
			{
				queueRooms.Enqueue(batroom.Dequeue());
			}
			else
			{
				if (resroom.Count != 0)
					queueRooms.Enqueue(resroom.Dequeue());
			}
		}
	}


	/** Adds a line of rooms off of a randomly selected room in the given direction. */
	void AddRooms(Direction direction, int length) {
		Vector2I position = RandomUsedPosition ();
		for (int i=0; i<length; i++) {
			position = GetNextPosition (direction, position);
			if (!RoomInRange (position)) {
				break;
			} else if (GetRoom (position) == null) {
				GameObject room;
				room = getQueueRoom();
				AddRoom (position, room, direction);
			}
		}
	}

	void AddRoom(Vector2I position, GameObject room, Direction? direction) {
		SetRoom (position, room);
		AddAvailableRooms (position);
		SetDoors (position, new Doors());
		currNumRooms++;
		if (direction != null) {
			GetDoors (GetPrevPosition((Direction) direction, position)).AddDoor ((Direction) direction);
			GetDoors (position).AddDoor (DirectionMethods.OppositeDirection((Direction) direction));
		}
	}

	/**
	 * Adds adjacent positions as potential room locations, and
	 * marks the given position as a used position.
	 */
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

	/**
	 * Given a direciton and position, returns the next position in that direction.
	 */ 
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

	Direction AdjacentRoomDirection(Vector2I position) {
		Vector2I up = GetNextPosition (Direction.UP, position);
		if (RoomInRange (up) && GetRoom (up) != null) {
			return Direction.UP;
		}
		Vector2I down = GetNextPosition (Direction.DOWN, position);
		if (RoomInRange (down) && GetRoom (down) != null) {
			return Direction.DOWN;
		}
		Vector2I left = GetNextPosition (Direction.LEFT, position);
		if (RoomInRange (left) && GetRoom (left) != null) {
			return Direction.LEFT;
		}
		Vector2I right = GetNextPosition (Direction.RIGHT, position);
		if (RoomInRange (right) && GetRoom (right) != null) {
			return Direction.RIGHT;
		}
		return Direction.RIGHT;
	}

	/**
	 * Given a direciton and position, returns the previous position in that direction.
	 */
	Vector2I GetPrevPosition(Direction direction, Vector2I position) {
		return GetNextPosition(DirectionMethods.OppositeDirection(direction), position);
	}

	bool RoomInRange(Vector2I position) {
		return position.x >= 0 && position.x < rooms.GetLength (0) &&
			position.y >= 0 && position.y < rooms.GetLength (1);
	}

	GameObject GetRoom(Vector2I position) {
		return rooms [position.x, position.y];
	}

	void SetRoom(Vector2I position, GameObject room) {
		rooms [position.x, position.y] = room;
	}

	Doors GetDoors(Vector2I position) {
		return this.doors [position.x, position.y];
	}

	void SetDoors(Vector2I position, Doors doors) {
		this.doors [position.x, position.y] = doors;
	}

	/**
	 * Randomly selects a used position.
	 */
	Vector2I RandomUsedPosition() {
		Vector2I[] usedPositionArray = new Vector2I[usedPositions.Count];
		usedPositions.CopyTo (usedPositionArray);
		return usedPositionArray [Random.Range (0, usedPositionArray.Length)];
	}

	/**
	 * Randomly selects an available position.
	 */
	Vector2I RandomAvailablePosition() {
		Vector2I[] availablePositionArray = new Vector2I[availablePositions.Count];
		availablePositions.CopyTo (availablePositionArray);
		return availablePositionArray [Random.Range (0, availablePositionArray.Length)];
	}

	/**
	 * Given a set of rooms, returns a random room from the set.
	 */
	GameObject getQueueRoom() {
		return queueRooms.Dequeue();
	}

	/**
	 * Returns a direction. I found that predictable rotation gave better
	 * variety, so it isn't currently random.
	 */ 
	Direction RandomDirection() {
		direction++;
		return (Direction) (direction % 4);
	}

	/**
	 * Instantiates all of the rooms, with central room placed at (0,0,0).
	 */
	void InstantiateRooms() {
		for (int i=0; i<rooms.GetLength (0); i++) {
			for (int j=0; j<rooms.GetLength (1); j++) {
				if (rooms[i, j] != null) {
					GameObject room = Instantiate (rooms[i, j]);
					Vector3 position = new Vector3((i-numRooms/2)*(roomWidth), (j-numRooms/2)*(roomHeight), 0);
					room.transform.position = position;
					Doors doors = this.doors [i, j];
					room.GetComponent<RoomController>().SetDoors(doors);
				}
			}
		}
	}
}
