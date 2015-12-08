using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int startingHealth = 100;            // The amount of health the enemy starts the game with.
    public int currentHealth;                   // The current health the enemy has.
    public int currentSize;                     // Size of the boss
    public float sinkSpeed = 2.5f;              // The speed at which the enemy sinks through the floor when dead.
    public int scoreValue = 10;                 // The amount added to the player's score when the enemy dies.
    public GameObject bossClone;                // Boss prefab

    Animator anim;                              // Reference to the animator.
    public AudioClip deathClip;                 // The sound to play when the enemy dies.
	public GameObject useAtDeathSound;
    AudioSource enemyAudio;                     // Reference to the audio source.
    ParticleSystem hitParticles;                // Reference to the particle system that plays when the enemy is damaged.
    CapsuleCollider capsuleCollider;            // Reference to the capsule collider.
    bool isDead;                                // Whether the enemy is dead.
    bool isSinking;                             // Whether the enemy has started sinking through the floor.

    void Awake()
    {
        // Setting up the references.
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        // Setting the current health when the enemy first spawns.
        currentHealth = startingHealth;
    }

    void Update()
    {
        // If the enemy should be sinking...
        if (isSinking)
        {
            // ... move the enemy down by the sinkSpeed per second.
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(int amount)
    {
        // If the enemy is dead...
        if (isDead)
            // ... no need to take damage so exit the function.
            return;

        // Play the hurt sound effect.
        if (enemyAudio != null)
        {
            enemyAudio.Play();
        }

        // Reduce the current health by the amount of damage sustained.
        currentHealth -= amount;

        // Set the position of the particle system to where the hit was sustained.
        //hitParticles.transform.position = hitPoint;

        // And play the particles.
        //hitParticles.Play();

        // If the current health is less than or equal to zero...
        if (currentHealth <= 0)
        {
            // ... the enemy is dead.
            Death();
        }
    }


    void Death()
    {

        // Turn the collider into a trigger so shots can pass through it.
        //capsuleCollider.isTrigger = true;

        // Tell the animator that the enemy is dead.
        //anim.SetTrigger ("Dead");

        // Change the audio clip of the audio source to the death clip and play it (this will stop the hurt clip playing).
        //enemyAudio.clip = deathClip;
		GameObject deathSound = (GameObject)Instantiate (this.useAtDeathSound, this.transform.position, Quaternion.identity);
        //enemyAudio.Play ();

        currentSize = currentSize / 2;
        startingHealth = startingHealth / 2;
        if (currentSize < 50)
        {
            Destroy(gameObject);
        }
        else
        {
            GameObject clone1 = Instantiate(bossClone, transform.position * 1.01f, transform.rotation) as GameObject;
			clone1.transform.parent = transform.parent;
            GameObject clone2 = Instantiate(bossClone, transform.position * 0.99f, transform.rotation) as GameObject;
			clone2.transform.parent = transform.parent;

            clone1.transform.localScale = new Vector2(currentSize, currentSize);
            clone2.transform.localScale = new Vector2(currentSize, currentSize);

            Destroy(gameObject);
        }
    }

    public void StartSinking()
    {
        // Find and disable the Nav Mesh Agent.
        GetComponent<NavMeshAgent>().enabled = false;

        // Find the rigidbody component and make it kinematic (since we use Translate to sink the enemy).
        GetComponent<Rigidbody>().isKinematic = true;

        // The enemy should no sink.
        isSinking = true;

        // Increase the score by the enemy's score value.
        //ScoreManager.score += scoreValue;

        // After 2 seconds destory the enemy.
        Destroy(gameObject, 2f);
    }
}