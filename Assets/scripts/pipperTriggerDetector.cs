using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pipperTriggerDetector : MonoBehaviour
{
    [SerializeField]
    hackingGame hackingGame;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("obsticle"))
            hackingGame.wallCollision();

        else if(collision.gameObject.CompareTag("Finish"))
            hackingGame.finishCollsion();
    }
}
