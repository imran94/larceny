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


    //Co-routine for moving units from one space to next, takes a parameter end to specify where to move to.
    protected IEnumerator SmoothMovement(int xDir, int zDir, Vector3 end)
    {
        Debug.Log("xDir: " + xDir + ", zDir: " + zDir);
        if (zDir != 0)
        {
            Debug.Log("Moving up/down");
            float remainingDistance = Mathf.Abs((transform.position - end).z);

            while (remainingDistance > 0.001f)
            {
                Vector3 newPosition = Vector3.MoveTowards(rb.position, end, inverseMoveTime * Time.deltaTime);
                rb.MovePosition(newPosition);
                remainingDistance = Mathf.Abs((transform.position - end).z);
                yield return null;
            }
        }
        else
        {
            Debug.Log("Moving left/right");
            float remainingDistance = Mathf.Abs((transform.position - end).x);

            while (remainingDistance > 0.001f)
            {
                Vector3 newPosition = Vector3.MoveTowards(rb.position, end, inverseMoveTime * Time.deltaTime);
                rb.MovePosition(newPosition);
                remainingDistance = Mathf.Abs((transform.position - end).x);
                yield return null;
            }
        }

        GameManager.instance.playersTurn = false;
    }

    //The virtual keyword means AttemptMove can be overridden by inheriting classes using the override keyword.
    //AttemptMove takes a generic parameter T to specify the type of component we expect our unit to interact with if blocked (Player for Enemies, Wall for Player).
    protected virtual void AttemptMove<T>(int xDir, int zDir)
        where T : Component
    {

    }
}