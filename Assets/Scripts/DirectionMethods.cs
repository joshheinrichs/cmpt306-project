using UnityEngine;
using System.Collections;

/**
 * Some utility methods for the Direction enum
 */
public class DirectionMethods {

	/**
	 * Returns the opposite of the given direction
	 */
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
