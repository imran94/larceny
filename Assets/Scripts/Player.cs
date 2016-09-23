using UnityEngine;
using System.Collections;
using System;

public class Player : MovingObject {

    public float restartLevelDelay = 1f;
    public AudioClip SFX_Collision;     //Specifying audio file
    public AudioClip SFX_Movement;        //Specifying audio file

    private Animator animator;
    private AudioSource source;

    // Used to store location of screen touch origin for mobile controls
    private Vector3 touchOrigin = -Vector3.one;
    private bool couldBeSwipe;
    private float swipeStartTime;
    private Vector2 startPos;
    private int stationaryForFrames;
    private TouchPhase lastPhase;

    private string text = "";

    private float minSwipeDist = 3;
    private float maxSwipeTime = 10;

    public RaycastHit hit;
    public bool input;

    protected override void Start()
    {
        animator = GetComponent<Animator>();

        source = GetComponent<AudioSource>();
        SFX_Movement = Resources.Load("SFX_Movement") as AudioClip;
        SFX_Collision = Resources.Load("SFX_Collision") as AudioClip;

        input = true;
        couldBeSwipe = false;
        base.Start();
    }

    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 50, 50), text);
    }

    protected override void Update()
    {
        // Exit the function if it's not the player's turn
        if (!GameManager.instance.playersTurn) return;
        if (!input) return;

        if ((int)Mathf.Round(transform.rotation.y) % 90 != 0)
            if (!rotating)
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        base.Update();
        //transform.position = new Vector3((Mathf.Round(transform.position.x)), transform.position.y, Mathf.Round(transform.position.z));

        int horizontal = 0;     // Horizontal move direction
        int vertical = 0;       // Vertical move direction

        // Get input from the input manager, round it to an integer and store in horizontal to set x axis move direction
        horizontal = (int)(Input.GetAxisRaw("Horizontal"));
        // Get input from the input manager, round it to an integer and store in vertical to set y axis move direction
        vertical = (int)(Input.GetAxisRaw("Vertical"));

#if UNITY_ANDROID

        if (Input.touchCount > 0)
		{
			//Store the first touch detected
			Touch myTouch = Input.touches[0];

			if (myTouch.phase == TouchPhase.Began)
			{
				text = "Touched";
				startPos = myTouch.position;
				couldBeSwipe = true;
				stationaryForFrames = 0;
				swipeStartTime = Time.time;
			}
			if (myTouch.phase == TouchPhase.Stationary)
			{
				text = "Stationary";
				//if (isContinuouslyStationary(6))
				//	couldBeSwipe = false;
			}
			if (myTouch.phase == TouchPhase.Ended)
			{
				text = "Ended";
				if (isASwipe(myTouch))
				{
					couldBeSwipe = false;

					float differenceX = myTouch.position.x - startPos.x;
					float differenceY = myTouch.position.y - startPos.y;

					text=("differenceX: " + differenceX + ", differenceY: " + differenceY); 

					if (Mathf.Abs(differenceX) > Mathf.Abs(differenceY))
					{
						if (Mathf.Sign(differenceX) == 1f)
						{
							text = "horizontal+";
							horizontal = 1;
						}
						else
						{
							text = "horizontal-";
							horizontal = -1;
						}
					}
					else
					{
						if (Mathf.Sign (differenceY) == 1f)
						{
							text = "vertical+";
							vertical = 1;
						}
						else
						{
							text = "vertical-";
							vertical = -1;
						}
					}

				}
			}
		}
#endif

        // text = "horizontal: " + horizontal.ToString () + ", vertical: " + vertical.ToString();

        //if (horizontal != 0)
        //vertical = 0;

        if (horizontal != 0 || vertical != 0)
        {
            if (Move(horizontal, vertical))
            {
                input = false;
                source.PlayOneShot(SFX_Movement, 1F);
            }
            //AttemptMove<TileType>(horizontal, vertical);
        }
    }

    private bool isContinuouslyStationary(int frames)
    {
        if (lastPhase == TouchPhase.Stationary)
            stationaryForFrames++;
        else
            stationaryForFrames = 1;

        return stationaryForFrames > frames;
    }

    private bool isASwipe(Touch touch)
    {
        float swipeTime = Time.time - swipeStartTime;
        float swipeDistX = Mathf.Abs(touch.position.x - startPos.x);
        float swipeDistY = Mathf.Abs(touch.position.y - startPos.y);

        return couldBeSwipe && swipeTime < maxSwipeTime && (swipeDistX > minSwipeDist 
                                                            || swipeDistY > minSwipeDist);
	}

    void OnCollisionEnter(Collision collision)
    {
        if (GameManager.instance.colliding) return;

        if (GameManager.instance.playersTurn && !GameManager.instance.enemiesMoving &&
            (collision.gameObject.name == "Guard(Clone)" || collision.gameObject.name == "Patrol(Clone)"))
        {
            GameManager.instance.colliding = true;
            Debug.Log("Player collision, playersturn: " + GameManager.instance.playersTurn +
                ", enemiesMoving: " + GameManager.instance.enemiesMoving);
            Destroy(collision.gameObject);
            source.PlayOneShot(SFX_Collision, 1F); //Play collision sound
            GameManager.instance.colliding = false;
            Debug.Log("Collision sound played");
        }
    }
}
