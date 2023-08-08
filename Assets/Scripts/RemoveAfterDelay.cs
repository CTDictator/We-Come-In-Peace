using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveAfterDelay : MonoBehaviour
{
    // Remove particles after a delay period.
    private void Start()
    {
        Destroy(gameObject, 1);
    }
}
