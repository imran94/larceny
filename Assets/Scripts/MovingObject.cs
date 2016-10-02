using UnityEngine;
using System.Collections;

//The abstract keyword enables you to create classes and class members that are incomplete and must be implemented in a derived class.
public abstract class MovingObject : MonoBehaviour
{
    public float moveTime;           //Time it will take object to move, in seconds.
    public static float inverseMoveTime;          //Used to make movement more efficient.

    //private AudioSource source;

    public bool moving;
    public bool rotating;
    protected Rigidbody rb;               //The Rigidbody component attached to this object.

    protected virtual void Start()
    {
        //Get a component reference to this object's Rigidbody2D
        rb = GetComponent<Rigidbody>();

        //By storing the reciprocal of the move time we can use it by multiplying instead of dividing, this is more efficient.
        moveTime = 0.1f;
        inverseMoveTime = 1f / moveTime;

        moving = false;
        rotating = false;

        //source = GetComponent<AudioSource>();
    }

    protected virtual void Update()
    {
        if (!moving)
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

    protected float getRemainingDistance(char c, Vector3 end)
    {
        if (c == 'x')
            return Mathf.Abs((rb.position - end).x);
        else
            return Mathf.Abs((rb.position - end).z);
    }

    float startTime;
    //Co-routine for moving units from one space to next, takes a parameter end to specify where to move to.
    protected IEnumerator SmoothMovement(int xDir, int zDir, Vector3 end)
    {
        while (moving || rotating)
            yield return null;

        moving = true;
        startTime = Time.time;
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

        //Debug.Log("Time to complete movement: " + (Time.time - startTime));

        end = transform.localPosition + transform.forward;
        end.x = Mathf.Round(end.x);
        end.z = Mathf.Round(end.z);

        if (this is Patrol && !TileMap.tiles[(int)end.x, (int)end.z].isWalkable)
        {
            StartCoroutine(Rotate(180f));
        }

        if (this is Player)
        {
            GameManager.instance.playersTurn = false;
        }
        moving = false;
    }

    protected IEnumerator Rotate(float rotationAmount)
    {
        int i = 0;
        Quaternion previousRotation = rb.rotation;
        rotating = true;
        Quaternion finalRotation = Quaternion.Euler(0, rotationAmount, 0) * rb.rotation;
        //Debug.Log("rb.rotation.y: " + rb.rotation.y + ", finalRotation.y " + finalRotation.y);

        do
        {     
            rb.rotation = Quaternion.Lerp(rb.rotation, finalRotation, /*Time.deltaTime * speed*/0.3f);
            if (rb.rotation == previousRotation)
            {
                i++;
            }
            else
            {
                i = 0;
            }

            if (i >= 3)
                break;

            previousRotation = rb.rotation;
            yield return null;
        } while (Mathf.Abs(rb.rotation.y) != finalRotation.y);

        rotating = false;

        //Debug.Log("Time to complete rotation: " + (Time.time - startTime));
        //Debug.Log("rb.rotation: " + Mathf.Round(transform.rotation.y) + ", finalRotation " + Mathf.Round(finalRotation.y));
    }

    public IEnumerator explode(bool destroy)
    {
        rb.isKinematic = true;

        if (GetComponent<MeshFilter>() == null || GetComponent<SkinnedMeshRenderer>() == null)
        {
            yield return null;
        }

        if (GetComponent<Collider>())
        {
            GetComponent<Collider>().enabled = false;
        }

        Mesh M = new Mesh();
        if (GetComponent<MeshFilter>())
        {
            M = GetComponent<MeshFilter>().mesh;
        }
        else if (GetComponent<SkinnedMeshRenderer>())
        {
            M = GetComponent<SkinnedMeshRenderer>().sharedMesh;
        }

        Material[] materials = new Material[0];
        if (GetComponent<MeshRenderer>())
        {
            materials = GetComponent<MeshRenderer>().materials;
        }
        else if (GetComponent<SkinnedMeshRenderer>())
        {
            materials = GetComponent<SkinnedMeshRenderer>().materials;
        }

        Vector3[] verts = M.vertices;
        Vector3[] normals = M.normals;
        Vector2[] uvs = M.uv;
        for (int submesh = 0; submesh < M.subMeshCount; submesh++)
        {

            int[] indices = M.GetTriangles(submesh);

            for (int i = 0; i < indices.Length; i += 3)
            {
                Vector3[] newVerts = new Vector3[3];
                Vector3[] newNormals = new Vector3[3];
                Vector2[] newUvs = new Vector2[3];
                for (int n = 0; n < 3; n++)
                {
                    int index = indices[i + n];
                    newVerts[n] = verts[index];
                    newUvs[n] = uvs[index];
                    newNormals[n] = normals[index];
                }

                Mesh mesh = new Mesh();
                mesh.vertices = newVerts;
                mesh.normals = newNormals;
                mesh.uv = newUvs;

                mesh.triangles = new int[] { 0, 1, 2, 2, 1, 0 };

                GameObject GO = new GameObject("Triangle " + (i / 3));
                //GO.layer = LayerMask.NameToLayer("Particle");
                GO.transform.position = transform.position;
                GO.transform.rotation = transform.rotation;
                GO.AddComponent<MeshRenderer>().material = materials[submesh];
                GO.AddComponent<MeshFilter>().mesh = mesh;
                //GO.AddComponent<BoxCollider>();
                Vector3 explosionPos = new Vector3(transform.position.x + UnityEngine.Random.Range(-0.5f, 0.5f), transform.position.y + UnityEngine.Random.Range(0f, 0.5f), transform.position.z + UnityEngine.Random.Range(-0.5f, 0.5f));

                GO.AddComponent<Rigidbody>().AddExplosionForce(UnityEngine.Random.Range(300, 500), explosionPos, 5, 1);
                //GO.AddComponent<Rigidbody>().AddExplosionForce()
                //Destroy(GO, 5 + UnityEngine.Random.Range(0.0f, 5.0f));
                Destroy(GO, 1.0f);
            }
        }

        GetComponent<Renderer>().enabled = false;

        //yield return new WaitForSeconds(1.0f);
        if (destroy)
        {
            Destroy(gameObject);
        }
    }

    //The virtual keyword means AttemptMove can be overridden by inheriting classes using the override keyword.
    //AttemptMove takes a generic parameter T to specify the type of component we expect our unit to interact with if blocked (Player for Enemies, Wall for Player).
    protected virtual void AttemptMove<T>(int xDir, int zDir)
        where T : Component
    {

    }
}