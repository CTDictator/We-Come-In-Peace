using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    // Game manager reference.
    private GameManager gameManager;
    // Variables for controlling the spaceships movement.
    [SerializeField] private Vector3 spaceshipDirection = Vector3.right;
    [SerializeField] private float spaceshipSpeed;
    [SerializeField] private float spaceshipDecend;
    // Variables for the spaceships weapon system.
    public GameObject laserPivot;
    public GameObject laserTargeter;
    public GameObject projectile;
    public Animator anim;
    [SerializeField] private float horizontalInput;
    [SerializeField] private float laserRotationSpeed;
    [SerializeField] private float maxLaserRotation;
    private static readonly float turretOffset = -0.6f;

    private void Start()
    {
        // Reference the game manager object for rules and boundaries.
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Move the spaceship across the x-axis and take in player inputs to switch directions and shoot.
    private void Update()
    {
        if (!gameManager.GameOver)
        {
            // Glide spaceship across the screen.
            GlideSpaceship();
            // Aim the spaceships weapon.
            AimSpaceshipLaser();
            // Fire the weapon.
            FireSpaceshipLaser();
        }
    }

    // Blow up the spaceship when colliding with a building.
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is against a building.
        if (collision.gameObject.CompareTag("Building"))
        {
            // Destroy the spaceship.
            DestroySpaceship();
        }
    }

    // Move the spaceship on screen.
    private void GlideSpaceship()
    {
        // Move the spaceship around so long as it is above the ground level.
        if (transform.position.y > gameManager.groundLevel)
        {
            // Move in an direction along the x-axis.
            transform.Translate(spaceshipDirection * Time.deltaTime * spaceshipSpeed);
            // Slowely lower the spaceship over time.
            transform.Translate(Vector3.down * Time.deltaTime * spaceshipDecend);
            // Switch directions on hitting boundaries.
            if (transform.position.x >= gameManager.xBoundaries)
            {
                transform.position = new Vector3(gameManager.xBoundaries, transform.position.y, transform.position.z);
                SwitchSpaceshipDirection();
            }
            else if (transform.position.x <= -gameManager.xBoundaries)
            {
                transform.position = new Vector3(-gameManager.xBoundaries, transform.position.y, transform.position.z);
                SwitchSpaceshipDirection();
            }
            // Switch directions on player input.
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                SwitchSpaceshipDirection();
            }
        }
        // Once the spaceship is on ground level, halt all its movements and declare victory.
        else
        {
            // Hide the targeter.
            laserTargeter.SetActive(false);
            // Declare victory as the player landed.
            gameManager.GameOver = true;
            anim.SetTrigger("HasWon");
            gameManager.CheckIfVictory();
        }
    }

    // Switch the spaceships direction on the x-axis.
    private void SwitchSpaceshipDirection()
    {
        spaceshipDirection *= -1.0f;
    }

    // Destroy the spaceship
    private void DestroySpaceship()
    {
        gameObject.SetActive(false);
        gameManager.GameOver = true;
        gameManager.CheckIfVictory();
    }

    // Aim the spaceships laser weaponry using a laser guide.
    private void AimSpaceshipLaser()
    {
        // Get the horizontal inputs & adjust the rotation speed.
        horizontalInput = Input.GetAxis("Horizontal") * Time.deltaTime * laserRotationSpeed;
        // Convert the inputs into the laser targeter rotation along the z-axis.     
        laserPivot.transform.Rotate(0.0f, 0.0f, horizontalInput);
        // Lock the laser targeters angle if it tries to exceed the maximum rotation.
        // Euler angles I swear to god ...
        if (laserPivot.transform.rotation.eulerAngles.z < 360.0f - maxLaserRotation
            && laserPivot.transform.rotation.eulerAngles.z > 180.0f)
        {
            laserPivot.transform.eulerAngles = new Vector3(0.0f, 0.0f, -maxLaserRotation);
        }
        else if (laserPivot.transform.rotation.eulerAngles.z > maxLaserRotation
            && laserPivot.transform.rotation.eulerAngles.z < 180.0f)
        {
            laserPivot.transform.eulerAngles = new Vector3(0.0f, 0.0f, maxLaserRotation);
        }
    }

    // Fire a laser shot at the angle the targetter is aiming at.
    private void FireSpaceshipLaser()
    {
        // When player hits spacebar, fire a projectile.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 turretPos = new(transform.position.x, transform.position.y + turretOffset, transform.position.z);
            Instantiate(projectile, turretPos, laserPivot.transform.rotation);
        }
    }
}
