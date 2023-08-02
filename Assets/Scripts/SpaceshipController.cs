using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    // Variables for controlling the spaceships movement.
    [SerializeField] private Vector3 spaceshipDirection = Vector3.right;
    [SerializeField] private float spaceshipSpeed;
    [SerializeField] private float spaceshipDecend;
    // Variables for the spaceships weapon system.
    public GameObject laserPivot;
    [SerializeField] private float horizontalInput;
    [SerializeField] private float laserRotationSpeed;
    [SerializeField] private float maxLaserRotation;

    // Move the spaceship across the x-axis and take in player inputs to switch directions and shoot.
    private void Update()
    {
        // Glide spaceship across the screen.
        GlideSpaceship();
        // Aim the spaceships weapon.
        AimSpaceshipLaser();
    }

    // Change directions on hitting game barriers.
    private void OnTriggerEnter(Collider other)
    {
        // Check if the trigger is a barrier.
        if (other.gameObject.CompareTag("Barrier"))
        {
            // Switch directions.
            SwitchSpaceshipDirection();
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
        // Move in an direction along the x-axis.
        transform.Translate(spaceshipDirection * Time.deltaTime * spaceshipSpeed);
        // Slowely lower the spaceship over time.
        transform.Translate(Vector3.down * Time.deltaTime * spaceshipDecend);
        // Switch directions and lower height on player input.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchSpaceshipDirection();
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
    }

    // Aim the spaceships laser weaponry using a laser guide.
    private void AimSpaceshipLaser()
    {
        // Get the horizontal inputs & adjust the rotation speed.
        horizontalInput = Input.GetAxis("Horizontal") * Time.deltaTime * laserRotationSpeed;
        // Convert the inputs into the laser targeter rotation along the z-axis.     
        laserPivot.transform.Rotate(0.0f, 0.0f, horizontalInput);
        // Lock the laser targeters angle if it tries to exceed the maximum rotation.
        if (laserPivot.transform.rotation.eulerAngles.z > maxLaserRotation)
        {
            // -> CONTINUE HERE.
        }
        else if (laserPivot.transform.rotation.eulerAngles.z < -maxLaserRotation)
        {

        }
    }
}
