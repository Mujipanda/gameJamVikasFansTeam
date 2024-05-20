using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitboxDetection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("obsticle"))
        {
            print("player hit a obsticle");
        }
    }
}
