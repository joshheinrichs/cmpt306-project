using UnityEngine;
using System.Collections;

public class DirectionMethods {
	public static Direction OppositeDirection(Direction direction) {
		switch (direction) {
		case Direction.UP:
			return Direction.DOWN;
		case Direction.DOWN:
			return Direction.UP;
		case Direction.LEFT:
			return Direction.RIGHT;
		default: //Direction.RIGHT:
			return Direction.LEFT;
		}
	}
}
