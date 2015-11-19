using UnityEngine;
using System.Collections;

/**
 * A simple class for tracking the doors for a given room
 */
public class Doors {

	public bool up, down, left, right;

	/**
	 * Constructs the doors for a new room, all initially set to false
	 */
	public Doors() {
		up = false;
		down = false;
		left = false;
		right = false;
	}

	/**
	 * Adds a door in the given direction
	 */
	public void AddDoor(Direction direction) {
		switch (direction) {
		case Direction.UP:
			up = true;
			break;
		case Direction.DOWN:
			down = true;
			break;
		case Direction.LEFT:
			left = true;
			break;
		case Direction.RIGHT:
			right = true;
			break;
		}
	}
}
