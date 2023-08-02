using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    // Variables for controlling the spaceship.
    [SerializeField] private Vector3 spaceshipDirection = Vector3.right;
    [SerializeField] private float spaceshipSpeed;
    
    // Move the spaceship across the x-axis and take in player inputs to drop a level and shoot.
    private void Update()
    {
        // Glide spaceship across the screen.
        GlideSpaceship();
    }

    // Change directions on hitting game barriers.
    private void OnTriggerEnter(Collider other)
    {
        // Check if the trigger is a barrier.
        if (other.gameObject.CompareTag("Barrier"))
        {
            // Switch directions.
            SwitchSpaceshipDirection();
            // Lower the spaceship height from the ground.
            LowerSpaceshipHeight();
        }
    }
    
    // Move the spaceship on screen.
    private void GlideSpaceship()
    {
        // Move in an direction along the x-axis.
        transform.Translate(spaceshipDirection * Time.deltaTime * spaceshipSpeed);
        // Switch directions and lower height on player input.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchSpaceshipDirection();
            LowerSpaceshipHeight();
        }
    }

    // Switch the spaceships direction on the x-axis.
    private void SwitchSpaceshipDirection()
    {
        spaceshipDirection *= -1.0f;
    }

    // Lower the spaceships height by a single unit.
    private void LowerSpaceshipHeight()
    {
        transform.Translate(Vector3.down);
    }
}
