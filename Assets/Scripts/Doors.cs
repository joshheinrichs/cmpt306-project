using UnityEngine;
using System.Collections;

public class Doors {
	public bool up, down, left, right;
	
	public Doors() {
		up = false;
		down = false;
		left = false;
		right = false;
	}

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
