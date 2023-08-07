using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarGoBrr : MonoBehaviour
{
    [SerializeField] private float carSpeed;
    private float xRange = 15.0f;

    // Cars simply move in 1 direction and destroys itself when out of range.
    private void Update()
    {
        transform.Translate(Vector3.right * carSpeed * Time.deltaTime);
        if (transform.position.x > xRange || transform.position.x < -xRange) Destroy(gameObject);
    }
}
