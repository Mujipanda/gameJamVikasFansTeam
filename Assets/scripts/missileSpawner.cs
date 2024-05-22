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
    [Range(1f, 10f)]
    [SerializeField]
    private float spawnDelayMin, spawnDelayMax;
    [SerializeField]
    private float detectDistance = 2;

    [SerializeField]
    private GameObject missilePrefab;

    [SerializeField]
    private GameObject explosionEffect;
    private bool canSpawn = true;

    [SerializeField]
    private List<GameObject> missiles = new List<GameObject>();
    private List<float> lifeTime = new List<float>();

    [SerializeField]
    private playerHealth health;

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
                createExplosion(i);
                health.takeDamage();
                Destroy(missiles[i]);
                missiles.Remove(missiles[i]);
                lifeTime.Remove(lifeTime[i]);
                
            }
            else if (lifeTime[i] > 20)
            {
                createExplosion(i);
                print(lifeTime[i] + " life time eached");
                Destroy(missiles[i]);
                missiles.Remove(missiles[i]);
                lifeTime.Remove(lifeTime[i]);
            }

        }
    }

    private void createExplosion(int i)
    {
        GameObject expo = Instantiate(explosionEffect, missiles[i].transform);
        expo.transform.parent = transform;
        StartCoroutine(destroyEffect(expo));
        audioManager.PlaySFX(audioManager.Explosion);
    }
    private IEnumerator destroyEffect(GameObject effect)
    {
        yield return new WaitForSeconds(2f);
        Destroy(effect);
    }

    IEnumerator spawnMissile()
    {
        canSpawn = false;
        GameObject missile = Instantiate(missilePrefab, spawnPoints[Random.Range(0,spawnPoints.Length)]);
        missiles.Add(missile);
        lifeTime.Add(0);

        audioManager.PlaySFX(audioManager.Missile);
        float delay = Random.Range(spawnDelayMin, spawnDelayMax);
        yield return new WaitForSeconds(delay);

       
        canSpawn = true;


    }

    public void removeMissile()
    {
        for(int i = 0; i < missiles.Count; i++)
        {
            if (!missiles[i].activeSelf) 
            {
                createExplosion(i);
                Destroy(missiles[i]);
                missiles.RemoveAt(i);
                
            }
        }
    }
}
