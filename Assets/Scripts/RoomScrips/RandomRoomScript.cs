using UnityEngine;
using System.Collections;

public class RandomRoomScript : MonoBehaviour {

	public Transform[] mustReachTiles; //specific tiles which the player must be able to get between


	string[,] toDo; //will hold what gameobject should be created in this tile[x][y]
	bool[,] isTileClear; //ture = this tile[x][y] is marked as a clear tile

	public int powerUpSpawnChance = 20; //chance in 100 for room to spawn a powerup (like a healthpack) object
	public GameObject powerUpSpawnObject; // the powerup object to spawn

	public GameObject wallSpawnObject; // what to spawn using makewall function (is done 1st)
	public int wallSpawnTargetNumber;  // number of times to try to add a wall tile
	public GameObject wallObjectParent; //the parent of wall object, should be baseroom (for organization
	public GameObject spikeSpawnObject; //what to spawn using makespike function 2nd
	public int spikeSpawnTargetNumber; // number of times to try to add a spike tile
	public GameObject spikeObjectParent; //should be obstacles, for organization and positioning
	public GameObject enemySpawnObject; //type of enemy to spawn using make enemies
	public int enemySpawnTargetNumber; // number of times to try to add an enemy
	public GameObject enemyObjectParent; //should be Enemies, for organization and positioning
	public GameObject otherObjectSpawn; // other object to try and add
	public int otherObjectTargetNumber; //number of times to try to add the other object
	public GameObject otherOgjectParent; //should be whatever is relevant (baseroom for wall things, obstacles for general non wall non enemy things, enemies for enemies, or something new if that is more relevant)

	public int clearContinueChance = 80; // the %chance in 100 that the clearing travel direction will have a chance to change before it reaches the target, or a wall

	private bool goLeft = false;
	private bool goRight = false;
	private bool goUp = false;
	private bool goDown = false;
	Transform curPos;

	const string wallCode = "Wall";
	const string spikeCode = "Spike";
	const string enemyCode = "Enemy";
	const string otherCode = "Other";
	const string powerCode = "Power";

	// Use this for initialization
	void Start () {
        this.isTileClear = new bool[25,15];
		//set all to false to start.
		for (int x = 0; x < this.isTileClear.GetLength(0); x++) {
			for (int y = 0; y < this.isTileClear.GetLength(1); y++) {
				this.isTileClear [x,y] = false;
			}
		}
		this.toDo = new string[25,15];
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

		//only make powerup if rolled
		if(Random.Range(0,101) <= this.powerUpSpawnChance)
			this.MakePowerUps ();

		this.InstatiateAll ();


	}

	void MakeClearPath()
	{
		ArrayList toReach = new ArrayList ();
		toReach.AddRange (this.mustReachTiles);


		//may not need the bools
		bool[] haveReached = new bool[(this.mustReachTiles.Length)];
		for (int i = 0; i < haveReached.Length; i++){
			haveReached[i] = false;
		}

		int randomInt = Random.Range (0, toReach.Count);

		//get a place to start form
		Transform startPosition = (Transform)toReach [randomInt];
		this.curPos = startPosition;
		//remove this from the places we need to reach
		toReach.RemoveAt (randomInt);



		bool fuck = false;
		//curPos.localPosition = startPosition.localPosition;

		//choose which direction to go
		while (toReach.Count > 0) {

			randomInt = Random.Range (0, toReach.Count);
			
			Transform endPosition = (Transform) toReach [randomInt];
			toReach.RemoveAt(randomInt);
			fuck = false;
			while (fuck == false){
			//(this.curPos.localPosition.x != endPosition.localPosition.x && this.curPos.localPosition.y != endPosition.localPosition.y) {
				this.ChooseDirection (endPosition);//choose direction to clear in
				this.GoClear (endPosition); //start clearing in that direction, until hit wall, or endposition, or randomchane to change direction

				if(this.curPos.localPosition.x == endPosition.localPosition.x && this.curPos.localPosition.y == endPosition.localPosition.y)
					fuck = true;
				else
					fuck = false;
			}

			Debug.Log("I think i reached a finish point!\n" +
				"cupos x = " + this.curPos.localPosition.x + " and y = " + this.curPos.localPosition.y + "\n" +
			          " and my target x = " + endPosition.localPosition.x + " and y = " + endPosition.localPosition.y + ".");
		}

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


			this.isTileClear[12 + (int)this.curPos.localPosition.x, 7 - (int)this.curPos.localPosition.y] = true; //clear this position

			this.curPos.localPosition = new Vector3(this.curPos.localPosition.x + xgo, this.curPos.localPosition.y + ygo, this.curPos.localPosition.z);

			//if we reached the target position, clear it, and break the loop
			if(this.curPos.localPosition.x == target.localPosition.x && this.curPos.localPosition.y == target.localPosition.y){
				this.isTileClear[12 + (int)this.curPos.localPosition.x, 7 - (int)this.curPos.localPosition.y] = true;
				break;
			}else if(Random.Range(1,101) >= this.clearContinueChance) //or random chance to stop going in this direction, choose direction should be called again
				break;

		}

		if (this.curPos.localPosition.x > 12)
			this.curPos.localPosition = new Vector3 (this.curPos.localPosition.x - 1, this.curPos.localPosition.y, this.curPos.localPosition.z);
		else if(this.curPos.localPosition.x < -12)
			this.curPos.localPosition = new Vector3 (this.curPos.localPosition.x + 1, this.curPos.localPosition.y, this.curPos.localPosition.z);
		else if(this.curPos.localPosition.y > 7)
			this.curPos.localPosition = new Vector3 (this.curPos.localPosition.x, this.curPos.localPosition.y -1 , this.curPos.localPosition.z);
		else if(this.curPos.localPosition.y < -7)
			this.curPos.localPosition = new Vector3 (this.curPos.localPosition.x, this.curPos.localPosition.y +1 , this.curPos.localPosition.z);

	}

	void MakeWalls()
	{
		/**
		for (int x = 0; x < this.toDo.GetLength(0); x ++) {
			for (int y = 0; y < this.toDo.GetLength(1); y ++) {
				if (this.isTileClear [x, y] == false && this.toDo [x, y] == null)
					this.toDo [x, y] = this.wallSpawnObject;
			}
		}
**/
		int x = -1;
		int y = -1;
		for (int i = 0; i < this.wallSpawnTargetNumber; i++) {
			x = Random.Range (0, this.toDo.GetLength (0));
			y = Random.Range (0, this.toDo.GetLength (1));
			if (this.isTileClear [x, y] == false && this.toDo [x, y] == null){
				this.toDo [x, y] = wallCode;
				//this.toDo[x,y].transform.parent = this.wallObjectParent.transform;
			}
		}

	}

	void MakeSpikes()
	{
		int x = -1;
		int y = -1;
		for (int i = 0; i < this.spikeSpawnTargetNumber; i++) {
			x = Random.Range (0, this.toDo.GetLength (0));
			y = Random.Range (0, this.toDo.GetLength (1));
			if (this.isTileClear [x, y] == false && this.toDo [x, y] == null){
				this.toDo [x, y] = spikeCode;
				//this.toDo[x,y].transform.parent = this.spikeOgjectParent.transform;
			}
		}
	}

	void MakeEnemyObjects()
	{
		int x = -1;
		int y = -1;
		for (int i = 0; i < this.enemySpawnTargetNumber; i++) {
			x = Random.Range (0, this.toDo.GetLength (0));
			y = Random.Range (0, this.toDo.GetLength (1));
			if (this.toDo [x, y] == null ){
				this.toDo [x, y] = enemyCode;
				//this.toDo[x,y].transform.parent = this.enemyObjectParent.transform;
			}
		}
	}

	void MakeOtherObjects()
	{
		int x = -1;
		int y = -1;
		for (int i = 0; i < this.otherObjectTargetNumber; i++) {
			x = Random.Range (0, this.toDo.GetLength (0));
			y = Random.Range (0, this.toDo.GetLength (1));
			if (this.isTileClear [x, y] == false && this.toDo [x, y] == null) {
				this.toDo [x, y] = otherCode;
				//this.toDo [x, y].transform.parent = this.otherOgjectParent.transform;
			}
		}

	}

	void MakePowerUps(){
		int x = -1;
		int y = -1;


		for(int tryTimes = 20; tryTimes > 0; tryTimes--){ //number of times to try finding valid spawn position if supposed to spawn powerup

			x = Random.Range (0, this.toDo.GetLength (0));
			y = Random.Range (0, this.toDo.GetLength (1));
			if (this.toDo [x, y] == null ){
				this.toDo [x, y] = powerCode;
				break;
				//this.toDo[x,y].transform.parent = this.enemyObjectParent.transform;
			}
		}

	}

	void InstatiateAll (){
		GameObject newObject;
		for (int x = 0; x < this.toDo.GetLength(0); x ++){
			for(int y = 0; y < this.toDo.GetLength(1); y ++){
				if(this.toDo[x,y] != null){
					switch(this.toDo[x,y]){
					case wallCode:
						newObject = (GameObject)Instantiate(this.wallSpawnObject,new Vector3(0,0,0),Quaternion.identity);
						newObject.transform.parent = this.wallObjectParent.transform;
						newObject.transform.localPosition = new Vector3(x-12, 7-y,1);
						break;
					case spikeCode:
						newObject = (GameObject)Instantiate(this.spikeSpawnObject,new Vector3(0,0,0),Quaternion.identity);
						newObject.transform.parent = this.spikeObjectParent.transform;
						newObject.transform.localPosition = new Vector3(x-12, 7-y,1);
						break;
					case enemyCode:
						newObject = (GameObject)Instantiate(this.enemySpawnObject,new Vector3(0,0,0),Quaternion.identity);
						newObject.transform.parent = this.enemyObjectParent.transform;
						newObject.transform.localPosition = new Vector3(x-12, 7-y,1);
						break;
					case otherCode:
						newObject = (GameObject)Instantiate(this.otherObjectSpawn,new Vector3(0,0,0),Quaternion.identity);
						newObject.transform.parent = this.otherOgjectParent.transform;
						newObject.transform.localPosition = new Vector3(x-12, 7-y,1);
						break;
					case powerCode:
						newObject = (GameObject)Instantiate(this.powerUpSpawnObject,new Vector3(0,0,0),Quaternion.identity);
						newObject.transform.parent = this.otherOgjectParent.transform;
						newObject.transform.localPosition = new Vector3(x-12, 7-y,1);
						break;
					default:
						//shouldn't happen, if it does, skip and do nothing
						break;
					}
				}
			}
		}
	}
	

}
