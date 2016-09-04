using UnityEngine;
using System.Collections;
using System;

public class Player : MovingObject {

    public float restartLevelDelay = 1f;

    private Animator animator;

	// Used to store location of screen touch origin for mobile controls
	private Vector3 touchOrigin = -Vector3.one;		
	private bool couldBeSwipe;
	private float swipeStartTime;
	private Vector2 startPos;
	private int stationaryForFrames;
	private TouchPhase lastPhase;

	private string text = "";

	private float minSwipeDist = 10;
	private float maxSwipeTime = 10;

    public RaycastHit hit;

    protected override void Start ()
    {
        animator = GetComponent<Animator>();

		couldBeSwipe = false;
        base.Start();
    }

	void OnGUI()
	{
		GUI.Label(new Rect(0, 0, 50, 50), text) ;
	}

	protected override void Update ()
    {
        // Exit the function if it's not the player's turn
		if (!GameManager.instance.playersTurn) return;

        transform.position = new Vector3((Mathf.Round(transform.position.x)), transform.position.y, Mathf.Round(transform.position.z));

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
            Move(horizontal, vertical);

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
		float swipeDist = Mathf.Abs (touch.position.x - startPos.x);

		return couldBeSwipe && swipeTime < maxSwipeTime && swipeDist > minSwipeDist;
	}

    void OnCollisionEnter(Collision collision)
    {
        if (GameManager.instance.playersTurn &&
            (collision.gameObject.name == "Guard(Clone)" || collision.gameObject.name == "Patrol(Clone)"))
        {
            Destroy(collision.gameObject);
        }
    }
}
