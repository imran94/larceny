using UnityEngine;
using System.Collections;

//The abstract keyword enables you to create classes and class members that are incomplete and must be implemented in a derived class.
public abstract class MovingObject : MonoBehaviour
{
    public static float moveTime = 0.1f;           //Time it will take object to move, in seconds.
    public LayerMask blockingLayer;         //Layer on which collision will be checked.

    private BoxCollider boxCollider;      //The BoxCollider component attached to this object.
    private float inverseMoveTime;          //Used to make movement more efficient.
    protected bool moving;
    protected Rigidbody rb;               //The Rigidbody component attached to this object.

    public float speed;

    //Protected, virtual functions can be overridden by inheriting classes.
    protected virtual void Start()
    {
        //Get a component reference to this object's BoxCollider2D
        boxCollider = GetComponent<BoxCollider>();
        
        //Get a component reference to this object's Rigidbody2D
        rb = GetComponent<Rigidbody>();
        //rb.isKinematic = true;

        //By storing the reciprocal of the move time we can use it by multiplying instead of dividing, this is more efficient.
        inverseMoveTime = 1f / moveTime;

        moving = false;
        speed = 0.2f;

    }

    protected virtual void Update()
    {
        transform.position = new Vector3((Mathf.Round(transform.position.x)), transform.position.y, Mathf.Round(transform.position.z));
    }

    protected virtual bool Move(int xDir, int zDir)
    {
        //Store start position to move from, based on objects current transform position.
        Vector3 start = transform.position;
        start.x = Mathf.Round(start.x);
        start.z = Mathf.Round(start.z);

        // Calculate end position based on the direction parameters passed in when calling Move.
        Vector3 end = start + new Vector3(xDir, 0f, zDir);
        end.x = Mathf.Round(end.x);
        end.z = Mathf.Round(end.z);

        Vector3 moveTo = start + new Vector3(xDir * 2, 0f, zDir * 2);

        if (TileMap.tiles[(int)end.x, (int)end.z].isWalkable)
        {
            StartCoroutine(SmoothMovement(xDir, zDir, moveTo));
            return true;
        }

        return false;
    }

    private float getRemainingDistance(char c, Vector3 end)
    {
        if (c == 'x')
            return Mathf.Abs((rb.position - end).x);
        else
            return Mathf.Abs((rb.position - end).z);
    }

    //Co-routine for moving units from one space to next, takes a parameter end to specify where to move to.
    protected IEnumerator SmoothMovement(int xDir, int zDir, Vector3 end)
    {
        char c;

        if (xDir != 0)
            c = 'x';
        else
            c = 'z';

        while (getRemainingDistance(c, end) > 0.001f)
        {
            Vector3 newPosition = Vector3.MoveTowards(rb.position, end, inverseMoveTime * Time.deltaTime);
            rb.MovePosition(newPosition);

            yield return null;
        }
        GameManager.instance.playersTurn = false;
    }

    protected IEnumerator Rotate(float rotationAmount)
    {
        Quaternion finalRotation = Quaternion.Euler(0, rotationAmount, 0) * rb.rotation;

        Debug.Log("transform.rotation: " + transform.rotation + " finalRotation: " + finalRotation);
        while (rb.rotation != finalRotation)
        {
            
            rb.rotation = Quaternion.Lerp(transform.rotation, finalRotation, /*Time.deltaTime * speed*/moveTime);
            yield return null;
        }
    }

    //The virtual keyword means AttemptMove can be overridden by inheriting classes using the override keyword.
    //AttemptMove takes a generic parameter T to specify the type of component we expect our unit to interact with if blocked (Player for Enemies, Wall for Player).
    protected virtual void AttemptMove<T>(int xDir, int zDir)
        where T : Component
    {

    }
}