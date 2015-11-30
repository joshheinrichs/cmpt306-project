using UnityEngine;
using System.Collections;

public class RandomRoomScript : MonoBehaviour {

	public Transform[] mustReachTiles; //specific tiles which the player must be able to get between


	GameObject[,] toDo; //will hold what gameobject should be created in this tile[x][y]
	bool[,] isTileClear; //ture = this tile[x][y] is marked as a clear tile

	public int clearContinueChance = 80; // the %chance in 100 that the clearing travel direction will have a chance to change before it reaches the target, or a wall

	private bool goLeft = false;
	private bool goRight = false;
	private bool goUp = false;
	private bool goDown = false;
	Transform curPos;

	// Use this for initialization
	void Start () {
        this.isTileClear = new bool[25,15];
		//set all to false to start.
		for (int x = 0; x < this.isTileClear.GetLength(0); x++) {
			for (int y = 0; y < this.isTileClear.GetLength(1); y++) {
				this.isTileClear [x,y] = false;
			}
		}
		this.toDo = new GameObject[25,15];
		//all gameobject should start non-existant
		for (int x = 0; x < this.toDo.GetLength(0); x++) {
			for (int y = 0; y < this.toDo.GetLength(1); y++) {
				this.toDo [x,y] = null;
			}
		}

		this.MakeClearPath ();
		this.MakeWalls ();
		this.MakeSpikes ();
		this.MakeEnemyObjects ();
		this.MakeOtherObjects ();


	}

	void MakeClearPath()
	{
		ArrayList toReach = new ArrayList ();
		toReach.AddRange (this.mustReachTiles);


		//may not need the bools
		bool[] haveReached = new bool[(this.mustReachTiles.Length + 1)];
		for (int i = 0; i < haveReached.Length; i++){
			haveReached[i] = false;
		}

		int randomInt = Random.Range (0, toReach.Count +1);

		//get a place to start form
		Transform startPosition = (Transform)toReach [randomInt];
		//remove this from the places we need to reach
		toReach.RemoveAt (randomInt);

		randomInt = Random.Range (0, toReach.Count + 1);

		Transform endPosition = (Transform) toReach [randomInt];
		toReach.RemoveAt(randomInt);

		this.curPos = startPosition;
		//curPos.localPosition = startPosition.localPosition;

		//choose which direction to go

		while (this.curPos.localPosition.x != endPosition.localPosition.x && this.curPos.localPosition.y != endPosition.localPosition.y) {
			this.ChooseDirection (endPosition);//choose direction to clear in
			this.GoClear (endPosition); //start clearing in that direction, until hit wall, or endposition, or randomchane to change direction
		}
		//once endposition reached, makesure it is removed from mustreach, then if anymore must reach remain, choose one and repeat



	}

	void ChooseDirection(Transform toReach)
	{
		this.goLeft = false;
		this.goRight = false;
		this.goUp = false;
		this.goDown = false;

		int lw = 10;
		int rw = 10;
		int uw = 10;
		int dw = 10;

		//if x to reach > cur x, set go right weight to 30, and left wight to 5 (and the oppisite case)
		if (toReach.localPosition.x > this.curPos.localPosition.x) {
			rw = 30;
			lw = 5;
		} else if (toReach.localPosition.x < this.curPos.localPosition.x) {
			lw = 30;
			rw = 5;
		}

		//if y to reach > cur y set go up weight to 30, and down weight to 5 (and the oppisite as well)
		if (toReach.localPosition.y > this.curPos.localPosition.y) {
			uw = 30;
			dw = 5;
		} else if (toReach.localPosition.y < this.curPos.localPosition.y) {
			dw = 30;
			uw = 5;
		}
		//in either case, if x or y to reach is == to cur, leave relivant wieghts at 10

		int totalWeight = lw + rw + uw + dw;
		int ranInt = Random.Range (1, totalWeight + 1);

		//if ran was <= lw, go left
		ranInt -= lw;
		if (ranInt <= 0) {
			this.goLeft = true;
			return;
		}
		//otherwise, if it was > lw, but <= rw, go right
		ranInt -= rw;
		if (ranInt <= 0) {
			this.goRight = true;
			return;
		}
		//otherwise, if it was > rw, but <= up, go up
		ranInt -= uw;
		if (ranInt <= 0) {
			this.goUp = true;
			return;
		}

		//if it wasn't any of the others, go down
		this.goDown = true;
		return;

	}

	void GoClear(Transform target){
		int xgo = 0;
		int ygo = 0;
		
		if (this.goUp)
			ygo = 1;
		else if (this.goDown)
			ygo = -1;
		else if (this.goRight)
			xgo = 1;
		else
			xgo = -1;
		
		while (this.curPos.localPosition.x <= 12 && this.curPos.localPosition.x >= -12 && this.curPos.localPosition.y <= 7 && this.curPos.localPosition.y >= -7) {


			this.isTileClear[(int)this.curPos.localPosition.x, (int)this.curPos.localPosition.y] = true; //clear this position

			this.curPos.localPosition = new Vector3(this.curPos.localPosition.x + xgo, this.curPos.localPosition.y + ygo, this.curPos.localPosition.z);

			//if we reached the target position, clear it, and break the loop
			if(this.curPos.localPosition.x == target.localPosition.x && this.curPos.localPosition.y == target.localPosition.y){
				this.isTileClear[(int)this.curPos.localPosition.x, (int) this.curPos.localPosition.y] = true;
				break;
			}else if(Random.Range(1,101) >= this.clearContinueChance) //or random chance to stop going in this direction, choose direction should be called again
				break;

		}

	}

	void MakeWalls()
	{



	}

	void MakeSpikes()
	{
	}

	void MakeEnemyObjects()
	{}

	void MakeOtherObjects()
	{}


	

}
