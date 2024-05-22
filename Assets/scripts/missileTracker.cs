using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missileTracker : MonoBehaviour
{
    [SerializeField]
    private float followDelay;
    private Transform playerPos;

    private Vector3 velocity = Vector3.zero;


    [SerializeField]
    private missileSpawner spawner;
    private void Start()
    {
        spawner = GetComponentInParent<missileSpawner>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        //StartCoroutine(missile());
    }


    void FixedUpdate()
    {
        
        float angle = Mathf.Atan2(playerPos.position.y - transform.position.y, playerPos.position.x - transform.position.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 50 * Time.fixedDeltaTime);

        transform.position = Vector3.SmoothDamp(transform.position, playerPos.position, ref velocity, followDelay * Time.deltaTime);
    }

    private IEnumerator missile()
    {
        float duration = 3;
        float timeElapsed = 0;
        while (timeElapsed < duration) 
        {
            float angle = Mathf.Atan2(playerPos.position.y - transform.position.y, playerPos.position.x - transform.position.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            float t = timeElapsed / duration;
         
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 50 * Time.fixedDeltaTime);
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, t);            
            timeElapsed += Time.fixedDeltaTime;     
            yield return new WaitForEndOfFrame();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("EMP"))
        {
            gameObject.SetActive(false);
            spawner.removeMissile();
        }
    }
}
