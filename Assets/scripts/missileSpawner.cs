using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missileSpawner : MonoBehaviour
{
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    [SerializeField]
    private Transform[] spawnPoints;


    [SerializeField]
    private Transform playerPos;

    [SerializeField]
    private float spawnDelay = 4;
    [SerializeField]
    private float detectDistance = 2;

    [SerializeField]
    private GameObject missilePrefab;


    private bool canSpawn = true;

    [SerializeField]
    private List<GameObject> missiles = new List<GameObject>();
    private List<float> lifeTime = new List<float>();

    private void calMissilePos()
    {
        float angle = Mathf.Atan2(playerPos.position.y - transform.position.y, playerPos.position.x - transform.position.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 50 * Time.fixedDeltaTime);
    }


   
    private void OnDrawGizmos()
    {
        for(int i = 0; i < spawnPoints.Length; i++)
        {
            Gizmos.color = Color.cyan;

            Matrix4x4 prevMatrix = Gizmos.matrix;
            Gizmos.matrix = Matrix4x4.TRS(spawnPoints[i].position, Quaternion.identity, Vector3.one);
            Gizmos.DrawSphere(Vector3.zero, 0.5f);
            Gizmos.matrix = prevMatrix;
        }
    }


    private void Update()
    {
        if(canSpawn)
        StartCoroutine(spawnMissile());

        for (int i = 0; i < missiles.Count; i++)
        {
            lifeTime[i] += Time.fixedDeltaTime;
            float dist = Vector2.Distance(missiles[i].transform.position, playerPos.position);
            //print(dist);
            if (dist < detectDistance)
            {
                Destroy(missiles[i]);
                missiles.Remove(missiles[i]);
                lifeTime.Remove(lifeTime[i]);
            }
            else if (lifeTime[i] > 30)
            {
                print(lifeTime[i] + " life time eached");
                Destroy(missiles[i]);
                missiles.Remove(missiles[i]);
                lifeTime.Remove(lifeTime[i]);
            }

        }

    }

    IEnumerator spawnMissile()
    {
        canSpawn = false;
        GameObject missile = Instantiate(missilePrefab, spawnPoints[Random.Range(0,spawnPoints.Length)]);
        missiles.Add(missile);
        lifeTime.Add(0);

        audioManager.PlaySFX(audioManager.Missile);

        yield return new WaitForSeconds(spawnDelay);

       
        canSpawn = true;


    }
}
