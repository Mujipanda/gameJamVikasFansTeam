using System.Collections;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class hackingGame : MonoBehaviour
{

    [SerializeField]
    private Transform startPos;

    [SerializeField]
    private GameObject pipper;


    private LineRenderer lineRend;


    [SerializeField]
    private float speed;

    private Quaternion rot;

    private Rigidbody2D rb;

    [SerializeField]
    MASTERCONTROLS masterControls;

    
    private GameObject trail;

   

    private bool settingNewLine = false;
    private void Start()
    {
        masterControls = new MASTERCONTROLS();
        masterControls.Enable();

        //masterControls.Player2.Movement2.performed += context => mov(context);
        rb = pipper.GetComponent<Rigidbody2D>();

        lineRend = gameObject.GetComponentInChildren<LineRenderer>();
        lineRend.useWorldSpace = true;
        if (lineRend == null)
            Debug.LogError("No Line Rendere Found in child of pipper");

        resetPipper();
        addPointToLine();
    }
    private void FixedUpdate()
    {
        if(pipper != null)
        {
            Vector3 dir = pipper.transform.up * speed * Time.deltaTime;
            rb.velocity = new Vector2(dir.x, dir.y);
            pipper.transform.rotation = rot;
        }
        // local position
       
        //print("banana");
        if(!settingNewLine)
        {
           // lineRend.SetPosition(lineRend.positionCount - 1, pipper.transform.position);
        }
       
    }

    /*
    private void mov(InputAction.CallbackContext input)
    {

        //Vector2 movement = input.Get<Vector2>();
        Vector2 movement = input.ReadValue<Vector2>();

        float movX = movement.x;

        switch (movement.x)
        {

            case 1:
                rot = Quaternion.Euler(0, 0, -90);
                addPointToLine(); break;
            case -1:
                rot = Quaternion.Euler(0, 0, 90);
                addPointToLine(); break;
        }
        float movY = movement.y;
        switch (movement.y)
        {
            case 1:
                rot = Quaternion.identity;
                addPointToLine();
                break;
            case -1:
                rot = Quaternion.Euler(0, 0, 180);
                addPointToLine(); break;
        }

    }
    */

    public void wallCollision()
    {
        resetPipper();
        print("pipper hit wall");
    }

    public void finishCollsion()
    {
        StartCoroutine(empDurationChange());
        resetPipper();
        print("pipper hit finish");
    }

    IEnumerator empDurationChange()
    {
        empEffect.coolDown = 0;
        yield return new WaitForSeconds(4);
        empEffect.coolDown = 5;

    }

    void OnMovement2(InputValue input)
    {
        Vector2 movement = input.Get<Vector2>();

        float movX = movement.x;

        switch (movement.x)
        {

            case 1:
                rot = Quaternion.Euler(0, 0, -90);
                addPointToLine(); break;
            case -1:
                rot = Quaternion.Euler(0, 0, 90);
                addPointToLine(); break;
        }
        float movY = movement.y;
        switch (movement.y)
        {
            case 1:
                rot = Quaternion.identity;
                addPointToLine();
                break;
            case -1:
                rot = Quaternion.Euler(0, 0, 180);
                addPointToLine(); break;
        }
        print("OnMove");
    }
    private void OnDrawGizmos()
    {
        if (startPos != null)
        {
            Gizmos.color = Color.yellow;
            Matrix4x4 prevMatrix = Gizmos.matrix;
            Gizmos.matrix = Matrix4x4.TRS(startPos.position, Quaternion.identity, Vector3.one);
            Gizmos.DrawWireSphere(Vector2.zero, 0.2f);
            Gizmos.matrix = prevMatrix;
        }

        if (lineRend != null)
        {
            for (int i = 0; i < lineRend.positionCount; i++)
            {
                Gizmos.color = Color.white;
                Matrix4x4 prevMatrix = Gizmos.matrix;
                Gizmos.matrix = Matrix4x4.TRS(lineRend.GetPosition(i), Quaternion.identity, Vector3.one);
                print(lineRend.GetPosition(i) + " " + i);
                Gizmos.DrawSphere(Vector2.zero, 0.2f);
                Gizmos.matrix = prevMatrix;
            }
        }

    }

    private void resetPipper()
    {
        
        pipper.transform.position = startPos.position;
        pipper.transform.rotation = Quaternion.identity;
        pipper.GetComponentInChildren<TrailRenderer>().time = 0;
        pipper.GetComponentInChildren<TrailRenderer>().time = 2;

        /* for (int i = 0; i < lineRend.positionCount + 1; i++)
         {
             lineRend.positionCount --;
         }*/

    }

    private void addPointToLine()
    {
       // settingNewLine = true;
       // lineRend.positionCount++;
       // lineRend.SetPosition(lineRend.positionCount - 2, pipper.transform.position);
       // settingNewLine = false;
    }
}
