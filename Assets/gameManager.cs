using System.Collections;
using UnityEngine;

public class gameManager : MonoBehaviour
{

    private bool canJam = true;

    [SerializeField]
    private int jamingCooldown;
    private void Start()
    {
        
    }

    private void Update()
    {
        if(canJam) 
        StartCoroutine(signalJam());
    }

    private IEnumerator signalJam()
    {
        canJam = false;


        yield return new WaitForSeconds(jamingCooldown);
        canJam = true;
    }


}
