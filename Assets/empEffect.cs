using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class empEffect : MonoBehaviour
{

    private Dictionary<int, GameObject> particles = new Dictionary<int, GameObject>();

    [SerializeField]
    private int particleCount;

    [SerializeField]
    private GameObject psPrefab;

    private float rotatePos;
    
    private float circleRadius;


    private float offset;

    [SerializeField]
    missileSpawner missileSpawner;

    [SerializeField]
    float duration;
    static public float coolDown = 5;

    private GameObject colliderObj;

    private bool canPlayEffect = true;
    private void Start()
    {
        colliderObj = transform.GetChild(0).gameObject;
        colliderObj.SetActive(false);
        for (int i = 0; i < particleCount; i++)
        {
            GameObject particl = Instantiate(psPrefab, transform);

            particles.Add(i, particl);
            particles[i].SetActive(false);
        }
        //StartCoroutine(playEffect());
    }


    Vector3 calPos(int index)
    {
        float angleRad = Mathf.Rad2Deg * (index + rotatePos);

        float x = Mathf.Cos(angleRad) * circleRadius;
        float y = Mathf.Sin(angleRad) * circleRadius;

        return gameObject.transform.position + new Vector3(x, y, 0);
    }

    private void circleCollider()
    {
        Collider[] hits;
        hits = Physics.OverlapSphere(transform.position, circleRadius);
        foreach (Collider hit in hits)
        {
            print(hit.name);
            if (hit.gameObject.CompareTag("missile"))
            {

                hit.gameObject.SetActive(false);
                //missileSpawner.removeMissile();

            }
        }
    }
    private IEnumerator playEffect()
    {
        canPlayEffect = false;
        colliderObj.SetActive(true);
        float elapsedTime = 0;
     
        while (elapsedTime < duration)
        {
            
            float t = elapsedTime / duration;

            
            circleRadius = Mathf.Lerp(0, 10, t);
            offset = Mathf.Lerp(0, 30, t);
            colliderObj.GetComponent<CircleCollider2D>().radius = circleRadius * 2;
            rotatePos = -Camera.main.transform.transform.rotation.eulerAngles.y - offset;

            for (int i = 0; i < particleCount; i++)
            {
                particles[i].SetActive(true);
                Vector3 pos = calPos(i);
                float angle = Mathf.DeltaAngle(i + rotatePos, gameObject.transform.eulerAngles.y);

                particles[i].transform.position = pos;
                particles[i].transform.rotation = Quaternion.Euler(0f, 0f, -angle);
                Vector3 scale = (Vector3.one * circleRadius) / 2;
                particles[i].transform.localScale = scale;
            }
            circleCollider();

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        for (int i = 0; i < particleCount; i++)
        {
            particles[i].SetActive(false);
        }
        circleRadius = 0;
        offset = 0;
        colliderObj.GetComponent<CircleCollider2D>().radius = 0;
        colliderObj.SetActive(false);
      
        yield return new WaitForSeconds(coolDown);
        canPlayEffect = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        /*for (int i = 0; i < particleCount; i++)
        {

            Vector3 pos = calPos(i);
            Matrix4x4 prev = Gizmos.matrix;
            Gizmos.matrix = Matrix4x4.TRS(pos, Quaternion.identity, Vector3.one);
            Gizmos.DrawWireSphere(pos, 0.1f);
            Gizmos.matrix = prev;
        }*/
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(transform.position, circleRadius);
    }

    void OnEmp()
    {
        startEMP();
    }
    public void startEMP()
    {
        if(canPlayEffect)
        StartCoroutine(playEffect());
    }
}
