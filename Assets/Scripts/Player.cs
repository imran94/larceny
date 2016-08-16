using UnityEngine;
using System.Collections;

public class Player : MovingObject {

    public float restartLevelDelay = 1f;

    private Animator animator;

    public RaycastHit hit;

    protected override void Start ()
    {
        animator = GetComponent<Animator>();
        base.Start();
    }
	
	private void Update ()
    {
        // Exit the function if it's not the player's turn
        if (!GameManager.instance.playersTurn) return;

        int horizontal = 0;     // Horizontal move direction
        int vertical = 0;       // Vertical move direction

        // Get input from the input manager, round it to an integer and store in horizontal to set x axis move direction
        horizontal = (int)(Input.GetAxisRaw("Horizontal"));
        // Get input from the input manager, round it to an integer and store in vertical to set y axis move direction
        vertical = (int)(Input.GetAxisRaw("Vertical"));

        if (horizontal != 0)
            vertical = 0;

        if (horizontal != 0 || vertical != 0)
            Move(horizontal, vertical);
            //AttemptMove<TileType>(horizontal, vertical);
    }

	protected override void OnCantMove<T>(T component)
    {

    }

}
