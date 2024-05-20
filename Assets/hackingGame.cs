using UnityEngine;
using UnityEngine.InputSystem;

public class hackingGame : MonoBehaviour
{

    private Vector2 direction = Vector2.zero;

    [SerializeField]
    private GameObject pipper;
    private void FixedUpdate()
    {
        pipper.transform.position += new Vector3(direction.x, + direction.y, 0);   
    }


    private void Onmovement(InputValue input)
    {
        Vector2 movement = input.Get<Vector2>();
        direction = movement;
        print(direction);
    }
}
