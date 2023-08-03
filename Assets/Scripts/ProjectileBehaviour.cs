using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    // Projectile variables.
    [SerializeField] private float projectileSpeed;

    // When a projectile is instantiated, fire it at high speed.
    void Start()
    {
        gameObject.GetComponent<Rigidbody>().AddRelativeForce(0.0f, -projectileSpeed, 0.0f, ForceMode.Impulse);
    }
}
