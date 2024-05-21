using UnityEngine;
using UnityEngine.InputSystem;

public class hackingGame : MonoBehaviour
{

    private Vector2 direction = Vector2.zero;

    [SerializeField]
    private GameObject pipper;


    private Rigidbody2D rb;

    [SerializeField]
    private float speed;

    private Quaternion rot;

    MASTERCONTROLS masterControls;

    private void Start()
    {
        masterControls = new MASTERCONTROLS();
        masterControls.Enable();

        masterControls.Player2.movementPl2.performed += context => OnmovementPl2(context);
        rb = pipper.GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        // local position
        Vector3 dir = pipper.transform.up * speed * Time.deltaTime;
        rb.velocity = new Vector2(dir.x, dir.y);
        pipper.transform.rotation = rot;
        //print("banana");

    }

    private void OnmovementPl2(InputAction.CallbackContext input)
    {


        //Vector2 movement = input.Get<Vector2>();
        Vector2 movement = input.ReadValue<Vector2>();
        direction = movement;

        print(movement + "Movemnt Vec");

        float movX = movement.x;

        switch (movement.x)
        {

            case 1: rot = Quaternion.Euler(0, 0, -90); break;
            case -1: rot = Quaternion.Euler(0, 0, 90); break;
        }
        float movY = movement.y;
        switch (movement.y)
        {
            case 1: rot = Quaternion.identity; break;
            case -1: rot = Quaternion.Euler(0, 0, 180); break;
        }
        print(direction + " Quaternion");
    }

}
