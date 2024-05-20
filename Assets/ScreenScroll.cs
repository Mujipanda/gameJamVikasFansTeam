using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenScroll : MonoBehaviour
{
    [SerializeField]
    private float Speed;
    private void FixedUpdate()
    {
        gameObject.transform.position = new Vector2(transform.position.x, transform.position.y + Time.deltaTime * Speed);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
