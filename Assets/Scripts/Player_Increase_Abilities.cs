using UnityEngine;
using System.Collections;

public class Player_Increase_Abilities : MonoBehaviour {

	GameObject player;
	PlayerHealth phealth;
	PlayerController pcontroller;
	ShootForward pshoot;

	public int skillpoints = 0;
	public int speedpoints = 0;
	public int damagepoints = 0;
	public int healthpoints = 0;

	public float speedChange = 25f;
	public int damageChange = 5;
	public int healthChange = 10;
	

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		phealth = player.GetComponent<PlayerHealth>();
		pcontroller = player.GetComponent<PlayerController>();
		pshoot = player.GetComponent<ShootForward>();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp("t"))
			modifyStat("spddown");
		if (Input.GetKeyUp("y"))
			modifyStat("spdup");
		if (Input.GetKeyUp("g"))
			modifyStat("dmgdown");
		if (Input.GetKeyUp("h"))
			modifyStat("dmgup");
		if (Input.GetKeyUp("b"))
			modifyStat("hpdown");
		if (Input.GetKeyUp("n"))
			modifyStat("hpup");
	}

	void modifyStat(string mod)
	{
		switch (mod)
		{
			case "dmgup":
				if (skillpoints != 0)
				{
					damagepoints++;
					skillpoints--;
					pshoot.changeBulletDamage(damageChange);
				}
				break;
			case "dmgdown":
				if (damagepoints != 0)
				{
					damagepoints--;
					skillpoints++;
					pshoot.changeBulletDamage(-damageChange);
				}
				break;
			case "spdup":
				if (skillpoints != 0)
				{
					speedpoints++;
					skillpoints--;
					pcontroller.changeSpeed(speedChange);
				}
				break;
			case "spddown":
				if (speedpoints != 0)
				{
					speedpoints--;
					skillpoints++;
					pcontroller.changeSpeed(-speedChange);
				}
				break;
			case "hpup":
				if (skillpoints != 0)
				{
					healthpoints++;
					skillpoints--;
					phealth.changeMaxHP(healthChange);
				}
				break;
			case "hpdown":
				if (healthpoints != 0)
				{
					healthpoints--;
					skillpoints++;
					phealth.changeMaxHP(-healthChange);

				}
				break;
			default:
				Debug.Log("Error: some sort of weird input passed into abilities.");
				break;

		}
	}
}
