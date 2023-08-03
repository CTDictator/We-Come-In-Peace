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
        StartCoroutine(ProjectileExpiration());
    }

    // Destroy the projectile on impact.
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    // If the projectile doesn't hit anything after some time, destroy it.
    IEnumerator ProjectileExpiration()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
