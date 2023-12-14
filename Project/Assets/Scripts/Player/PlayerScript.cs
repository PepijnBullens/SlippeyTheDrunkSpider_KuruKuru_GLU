using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header ("Bottle State")]

    [SerializeField]
    private bool bottleState;


    [Header("GameObjects")]

    [SerializeField]
    private new GameObject camera;
    private GameObject bottle;


    [Header("Scripts")]

    [SerializeField]
    private PlayerCollisionScript collisionScript;


    [Header("Speed Variables")]

    [SerializeField]
    private float bottleRotateSpeed;
    [SerializeField]
    private float playerRotateSpeed;

    [SerializeField]
    private float cameraMoveSpeed;
    [SerializeField]
    private float playerMoveSpeed;


    [Header("Look Directions")]

    [SerializeField]
    private float lookLeftDirection;
    [SerializeField]
    private float lookRightDirection;


    [Header("Particle Systems")]
    [SerializeField]
    private ParticleSystem explosion;


    private Vector2 startPos;

    //can move variable
    private bool canMove = true;

    //keep hold of when to rotate bottle and when not to
    private bool rotateBottle = true;

    //rigidbody's
    private Rigidbody2D playerRigidbody;
    private Rigidbody2D bottleRigidbody;

    //timer manager script
    private TimerManagerScript timerManagerScript;


    // start
    private void Start()
    {
        startPos = transform.position;

        //objects only made once and easy to find in scene are referenced by "GameObject.Find()"

        //reference timer manager script
        timerManagerScript = GameObject.Find("TimerManager").GetComponent<TimerManagerScript>();

        //bottle reference
        bottle = transform.GetChild(0).gameObject;

        /* rigidbody's */
        bottleRigidbody = bottle.GetComponent<Rigidbody2D>();
        playerRigidbody = GetComponent<Rigidbody2D>();

        bottle.SetActive(bottleState);
    }

    //fixed update
    private void FixedUpdate()
    {
        //set bottle's position to players position
        bottle.transform.position = transform.position;

        //rotate bottle
        if(rotateBottle)
        {
            bottleRigidbody.rotation += bottleRotateSpeed * Time.fixedDeltaTime;
        }

        /* move camera to player smoothly */
        camera.transform.position = Vector3.MoveTowards(camera.transform.position, transform.position, cameraMoveSpeed);

        //if not allowed to move stop the update
        if (!canMove) return;

        /* --------------- movement and rotation --------------- */
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            //if timer is not running
            if(!timerManagerScript.GetTimerState())
            {
                //start timer
                timerManagerScript.SetTimerState(true);
            }
        }

        if(Input.GetAxis("Horizontal") > 0)
        {
            //check if allowed to rotate
            if(playerRigidbody.rotation > lookRightDirection)
            {
                //rotate
                playerRigidbody.rotation -= playerRotateSpeed * Time.fixedDeltaTime;
            }

            //move
            playerRigidbody.AddForce(new Vector2(playerMoveSpeed, 0));
        }
        else if(Input.GetAxis("Horizontal") < 0)
        {
            //check if allowed to rotate
            if (playerRigidbody.rotation < lookLeftDirection)
            {
                //rotate
                playerRigidbody.rotation += playerRotateSpeed * Time.fixedDeltaTime;
            }

            //move
            playerRigidbody.AddForce(new Vector2(-playerMoveSpeed, 0));
        }

        if(Input.GetAxis("Vertical") > 0)
        {
            //move
            playerRigidbody.AddForce(new Vector2(0, playerMoveSpeed));
        }
        else if(Input.GetAxis("Vertical") < 0)
        {
            //move
            playerRigidbody.AddForce(new Vector2(0, -playerMoveSpeed));
        }
    }
    
    //get can move var
    public bool GetCanMove()
    {
        return canMove;
    }

    //set can move var
    public void SetCanMove(bool _canMove)
    {
        canMove = _canMove;
    }

    // reset player func
    public void RestartPlayer()
    {
        //disable explosion effect
        explosion.gameObject.SetActive(false);

        //reset players transform
        playerRigidbody.rotation = -180;
        playerRigidbody.velocity = Vector3.zero;
        transform.position = startPos;

        //allow player to move
        canMove = true;

        //reset bottle active state
        bottle.SetActive(bottleState);

        //enable collision
        bottle.GetComponent<BoxCollider2D>().enabled = true;

        //enable bottle rotation
        rotateBottle = true;
    }

    //stop movement func
    public void StopMovement()
    {
        //disable bottle rotation
        rotateBottle = false;
        //disable movement
        canMove = false;
        //set the velocity left on player to zero
        playerRigidbody.velocity = Vector3.zero;
    }

    //set bottle inactive or active func
    public void SetBottleState(bool state)
    {
        bottle.SetActive(state);
    }
}
