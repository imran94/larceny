using UnityEngine;
using System.Collections;

//The abstract keyword enables you to create classes and class members that are incomplete and must be implemented in a derived class.
public abstract class MovingObject : MonoBehaviour
{
    public float moveTime = 0.5f;           //Time it will take object to move, in seconds.
    public LayerMask blockingLayer;         //Layer on which collision will be checked.

    private BoxCollider boxCollider;      //The BoxCollider component attached to this object.
    private Rigidbody rb;               //The Rigidbody component attached to this object.
    private float inverseMoveTime;          //Used to make movement more efficient.
    protected bool moving;

    //Protected, virtual functions can be overridden by inheriting classes.
    protected virtual void Start()
    {
        //Get a component reference to this object's BoxCollider2D
        boxCollider = GetComponent<BoxCollider>();

        //Get a component reference to this object's Rigidbody2D
        rb = GetComponent<Rigidbody>();

        //By storing the reciprocal of the move time we can use it by multiplying instead of dividing, this is more efficient.
        inverseMoveTime = 1f / moveTime;

        moving = false;
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

		if (TileMap.tiles[(int) end.x, (int) end.z].isWalkable)
        {
            transform.position = start + new Vector3(xDir * 2, 0f, zDir * 2);

            GameManager.instance.playersTurn = false;
            //StartCoroutine(SmoothMovement(end));
            return true;
        }

        return false;
    }

    //Move returns true if it is able to move and false if not. 
    //Move takes parameters for x direction, y direction and a RaycastHit2D to check collision.
    protected bool Move(int xDir, int zDir, out RaycastHit hitInfo)
    {
        //Store start position to move from, based on objects current transform position.
        Vector3 start = transform.position;

        // Calculate end position based on the direction parameters passed in when calling Move.
        Vector3 end = start + new Vector3(xDir, 0, zDir);

        //Disable the boxCollider so that linecast doesn't hit this object's own collider.
        boxCollider.enabled = false;

        //Cast a line from start point to end point checking collision on blockingLayer.
        bool hitObject = Physics.Linecast(start, end, out hitInfo, blockingLayer);

        //Re-enable boxCollider after linecast
        boxCollider.enabled = true;

        //Check if nothing was hit
        if (!hitObject)
        {
        //If nothing was hit, start SmoothMovement co-routine passing in the Vector2 end as destination
            StartCoroutine(SmoothMovement(end));

        //Return true to say that Move was successful
            return true;
        }



        //If something was hit, return false, Move was unsuccesful.
        return false;
    }


    //Co-routine for moving units from one space to next, takes a parameter end to specify where to move to.
    protected IEnumerator SmoothMovement(Vector3 end)
    {
        //Calculate the remaining distance to move based on the square magnitude of the difference between current position and end parameter. 
        //Square magnitude is used instead of magnitude because it's computationally cheaper.
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;


        //While that distance is greater than a very small amount (Epsilon, almost zero):
        while (sqrRemainingDistance > float.Epsilon)
        {
            //Find a new position proportionally closer to the end, based on the moveTime
            Vector3 newPosition = Vector3.MoveTowards(transform.position, end, inverseMoveTime * Time.deltaTime);
            
            //Call MovePosition on attached Rigidbody2D and move it to the calculated position.
            rb.MovePosition(newPosition);

            //Recalculate the remaining distance after moving.
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            //Return and loop until sqrRemainingDistance is close enough to zero to end the function
            yield return null;
        }
    }


    //The virtual keyword means AttemptMove can be overridden by inheriting classes using the override keyword.
    //AttemptMove takes a generic parameter T to specify the type of component we expect our unit to interact with if blocked (Player for Enemies, Wall for Player).
    protected virtual void AttemptMove<T>(int xDir, int zDir)
        where T : Component
    {
        //Hit will store whatever our linecast hits when Move is called.
        RaycastHit hit;

        //Set canMove to true if Move was successful, false if failed.
        bool canMove = Move(xDir, zDir, out hit);

        //Check if nothing was hit by linecast
        if (hit.transform == null)
            //If nothing was hit, return and don't execute further code.
            return;

        //Get a component reference to the component of type T attached to the object that was hit
        T hitComponent = hit.transform.GetComponent<T>();
    }
}