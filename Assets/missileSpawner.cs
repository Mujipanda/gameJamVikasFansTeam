using System.Collections;
using UnityEngine;

public class missileSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform[] spawnPoints;


    [SerializeField]
    private Transform playerPos;

    [SerializeField]
    private float spawnDelay = 4;

    [SerializeField]
    private GameObject missilePrefab;


    private bool canSpawn = true;

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
    }

    IEnumerator spawnMissile()
    {
        canSpawn = false;
        GameObject missile = Instantiate(missilePrefab, spawnPoints[Random.Range(0,spawnPoints.Length)]);




            yield return new WaitForSeconds(spawnDelay);

        canSpawn = true;

    }
}
