using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class texthiden : MonoBehaviour
{
  
    void Start()
    {
        StartCoroutine(hiddentext());   
    }

   IEnumerator hiddentext()
    {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
