using UnityEngine;
using UnityEngine.UI;
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

	Text skillPoints;
	Text damage;
	Text health;
	Text speed;



	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		phealth = player.GetComponent<PlayerHealth>();
		pcontroller = player.GetComponent<PlayerController>();
		pshoot = player.GetComponent<ShootForward>();
		skillPoints = GameObject.Find("HUDCanvas/Levels/SkillPoints").GetComponent<Text>();
		damage = GameObject.Find("HUDCanvas/Levels/Damage").GetComponent<Text>();
		health = GameObject.Find("HUDCanvas/Levels/Health").GetComponent<Text>();
		speed = GameObject.Find("HUDCanvas/Levels/Speed").GetComponent<Text>();
		skillPoints.text = skillpoints.ToString();
		damage.text = damagepoints.ToString();
		speed.text = speedpoints.ToString();
		health.text = healthpoints.ToString();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp("t"))
			modifyStat("spddown");
		if (Input.GetKeyUp("y"))
			modifyStat("spdup");
		if (Input.GetKeyUp("b"))
			modifyStat("dmgdown");
		if (Input.GetKeyUp("n"))
			modifyStat("dmgup");
		if (Input.GetKeyUp("g"))
			modifyStat("hpdown");
		if (Input.GetKeyUp("h"))
			modifyStat("hpup");
	}


	public void increaseSkillPoints(int amount)
	{
		skillpoints += amount;
		skillPoints.text = skillpoints.ToString();
	}

	// changes the players stats (dmg, hp, speed)
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
					damage.text = damagepoints.ToString();
					skillPoints.text = skillpoints.ToString();
				}
				break;
			case "dmgdown":
				if (damagepoints != 0)
				{
					damagepoints--;
					skillpoints++;
					pshoot.changeBulletDamage(-damageChange);
					damage.text = damagepoints.ToString();
					skillPoints.text = skillpoints.ToString();
				}
				break;
			case "spdup":
				if (skillpoints != 0)
				{
					speedpoints++;
					skillpoints--;
					pcontroller.changeSpeed(speedChange);
					speed.text = speedpoints.ToString();
					skillPoints.text = skillpoints.ToString();
				}
				break;
			case "spddown":
				if (speedpoints != 0)
				{
					speedpoints--;
					skillpoints++;
					pcontroller.changeSpeed(-speedChange);
					speed.text = speedpoints.ToString();
					skillPoints.text = skillpoints.ToString();
				}
				break;
			case "hpup":
				if (skillpoints != 0)
				{
					healthpoints++;
					skillpoints--;
					phealth.changeMaxHP(healthChange);
					health.text = healthpoints.ToString();
					skillPoints.text = skillpoints.ToString();
				}
				break;
			case "hpdown":
				if (healthpoints != 0)
				{
					healthpoints--;
					skillpoints++;
					phealth.changeMaxHP(-healthChange);
					health.text = healthpoints.ToString();
					skillPoints.text = skillpoints.ToString();
				}
				break;
			default:
				Debug.Log("Error: some sort of weird input passed into abilities.");
				break;

		}
	}
}
