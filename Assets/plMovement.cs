using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class plMovement : MonoBehaviour
{
    [SerializeField]
    private Vector2 movVec;

    private Rigidbody rb;

    [SerializeField]
    private float speed;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void OnMovement(InputValue input)
    {
        Vector2 movement = input.Get<Vector2>();
        // movVec = new Vector2(movement.x, movement.y);

        StartCoroutine(lerpMovement(movement));
       
       
    }

    IEnumerator lerpMovement(Vector2 mov)
    {
        float elapsedTime = 0; ;
        float duration = 1f;
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            float x = Mathf.Lerp(movVec.x, mov.x, t);
            float y = Mathf.Lerp(movVec.y, mov.y, t);

            movVec = new Vector2(x,y);
            //print(movVec);
            rb.velocity = movVec * speed;
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();

        }
    }

}
