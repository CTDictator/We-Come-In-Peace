using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSegmentBehaviour : MonoBehaviour
{
    // Constants of the building segment.
    private static float buildingLinearVelocityMax = 1.0f;
    
    // If the building segment is falling or collides with a spaceship, destroy it.
    private void OnCollisionEnter(Collision collision)
    {
        // If the building segment is falling & hits something, blow it up.
        if (gameObject.GetComponent<Rigidbody>().velocity.magnitude > buildingLinearVelocityMax)
        {
            gameObject.SetActive(false);
        }
        // If the building segment is struck by a spaceship, also blow it up.
        else if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}
