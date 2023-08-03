using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BuildingSegmentBehaviour : MonoBehaviour
{
    // Building segment variables.
    [SerializeField] private int buildingIntegrity;
    [SerializeField] private bool freeFall;
    // Building segment cessible properties.
    public bool FreeFall
    {
        get { return freeFall; }
    }
    // Constants of the building segment.
    private static readonly float buildingLinearVelocityMax = 2.0f;

    // Remove the building segment from play when its integrity reaches zero.
    private void Update()
    {
        // Check if building is in freefall.
        freeFall = IsFreefalling();
        // Blow up building segment when integrity reaches zero.
        BlowUpBuildingSegment();
    }

    // If the building segment is falling or collides with a spaceship, destroy it.
    private void OnCollisionEnter(Collision collision)
    {
        // Remove all forces on the building segment.
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        // If the building segment is struck by a projectile, reduce integrity by 1 level.
        if (collision.gameObject.CompareTag("Projectile"))
        {
            --buildingIntegrity;
        }
        // If the building is in free fall ...
        else if (freeFall)
        {
            // If the building segment falls onto a non free falling segment, blow it up.
            if (collision.gameObject.CompareTag("Building") 
                && !collision.gameObject.GetComponent<BuildingSegmentBehaviour>().FreeFall)
            {
                buildingIntegrity = 0;
            }
            // If the building segment falls onto the ground, blow it up.
            else if (collision.gameObject.CompareTag("Ground"))
            {
                buildingIntegrity = 0;
            }
        }
        // If the building segment is struck by a spaceship, blow it up.
        else if (collision.gameObject.CompareTag("Player"))
        {
            buildingIntegrity = 0;
        }
    }

    // Check the building segments integrity and remove it from play when reaching zero.
    private void BlowUpBuildingSegment()
    {
        // If building segment reaches zero, remove it from play.
        if (buildingIntegrity <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    // Check the magnitude of the building to determine if it is in freefall or not.
    private bool IsFreefalling()
    {
        // Once an object starts freefalling, it is forever considered freefalling.
        if (gameObject.GetComponent<Rigidbody>().velocity.magnitude > buildingLinearVelocityMax)
            return true;
        if (freeFall)
            return true;
        return false;
    }
}
