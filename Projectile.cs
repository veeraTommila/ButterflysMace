using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float publicTime;
    public float damageRadius;
    public float damage;

    private Rigidbody rb;
    private float privateTime;
    public string shooterTag;
    public GameObject explotion;

    public PlayerController playerHealth;
    public GameObject Player;
    private float maximumHealth;
    private float currentHealth;
    private HealthBar healthBar;

    //private float damageTimer = 0;
    //public float damageDelay = 1;

    // Start is called before the first frame update
    void Start()
    {
        privateTime = publicTime;
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
        Player = GameObject.FindWithTag("Player"); // Sets the variable to the GameObject that has a "Player" tag.
        //currentHealth = maximumHealth;
        //healthBar.SetMaxHealth(maximumHealth);
    }

    // Update is called once per frame
    void Update()
    {
        privateTime -= Time.deltaTime;

        // If the time goes to zero, the projectile is destroyed.
        if (privateTime < 0)
        {
            
            //MakeDamage();
            Destroy(gameObject);
        }
        else
        {
            MakeDamage();
        }
    }

    // If the projectile hit's an object, the projectile is destroyed.
    private void MakeDamage()
    {       
        // Destroy the hitted objects and save the results to an array.
        Collider[] colliders = Physics.OverlapSphere(transform.position, damageRadius);
        
        for (int i = 0; i < colliders.Length; i++)
        {
           
            PlayerController playerHealth = colliders[i].GetComponent<PlayerController>();
            if (playerHealth != null)
            {
                //currentHealth--;
                playerHealth.TakeDamage(damage/3);
            }
            //playerHealth.TakeDamage(damage);
        }

        /*for (int i = 0; i < colliders.Length; i++)
        {

            HealthBar playerHealth = colliders[i].GetComponent<HealthBar>();
            if (playerHealth != null)
            {
                //currentHealth--;
                playerHealth.ReduceHealth(damage);
            }

        }*/
        //playerHealth.TakeDamage(damage);
        // OR

        /*for (int i = 0; i < colliders.Length; i++)
        {
            Health health = colliders[i].GetComponent<Health>();
            if (health != null)
            {
                health.ReduceHealth(damage);
            }
        }*/

        Instantiate(explotion, transform.position, new Quaternion());
        Destroy(gameObject);
    }

    

    // This function is called when the box collider touches an object (other).
    private void OnTriggerEnter(Collider other)
    {
        // The projectile is destroyed if the compared tag is not muzzle or its' shooter.
        if (!other.CompareTag("Muzzle")&& !other.CompareTag(shooterTag))
        {
            MakeDamage();
        }
        /*
        if(other.CompareTag("Player")) // If the object is the player, it plays a sound to warn the player they've been sensed.
        {
            //AudioSource EnemyStartUp = GetComponent<AudioSource>();
            //EnemyStartUp.Play();
            Debug.Log("Muzzle starts up.");
            playerHealth.TakeDamage(damage);
        }*/
    }

    // Called when a collision is detected 
    /*void OnCollisionEnter(Collision collision)
    {
        // Check if the collider that has collided with the enemy is the player 
        if (collision.collider.tag == "Player")
        {
            // Check if the player is currently able to take damage (i.e. the damage timer is less than or equal to zero) 
            if (damageTimer <= 0)
            {
                // Subtract the damage amount from the player's health 
                HealthBar playerHealth = collision.collider.GetComponent<HealthBar>();
                playerHealth.ReduceHealth(damage);

                // Reset the damage timer 
                damageTimer = damageDelay;
            }
        }
    }*/
}
