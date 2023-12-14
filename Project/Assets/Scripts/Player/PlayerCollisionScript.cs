using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionScript : MonoBehaviour
{
    [Header("Disable Or Enable Collision")]

    [SerializeField]
    private bool collision;

    [Header("Speed Variables")]

    [SerializeField]
    private float shrinkSpeed = 0.6f;
    [SerializeField]
    private float endSpeed = 0.6f;


    [Header("Script Reference's")]

    [SerializeField]
    private PlayerScript playerScript;


    [Header("Explosion Effect Variables")]

    [SerializeField]
    private ParticleSystem explosion;


    //keep track of when to shrink
    private bool shrinkEnabled = false;

    //variable to set start size
    private float startSize;
    //variable to set size to
    private float size;

    //player object
    private GameObject playerObject;

    /* end point vars */
    private Vector2 endPoint;
    private bool isAtEndPoint = false;

    //manager scripts
    private GameManagerScript gameManagerScript;
    private MenuManagerScript menuManagerScript;
    private TimerManagerScript timerManagerScript;

    //start
    private void Start()
    {
        //objects only made once and easy to find in scene are referenced by "GameObject.Find()"

        //reference game manager script
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        //reference menu manager script
        menuManagerScript = GameObject.Find("MenuManager").GetComponent<MenuManagerScript>();
        //reference timer manager script
        timerManagerScript = GameObject.Find("TimerManager").GetComponent<TimerManagerScript>();

        //reference player object through palyer script
        playerObject = playerScript.gameObject;

        //find end point by searching for it
        endPoint = GameObject.Find("EndPoint").transform.position;

        //set start size to current size
        startSize = transform.localScale.x; ;
        //set size to start size
        size = startSize;
    }

    //update
    private void Update()
    {
        //if shrinking is enabled shrink
        if(shrinkEnabled) Shrink();

        //if player is at end point
        if(isAtEndPoint) MovePlayerToEndPoint();
    }

    //reset collision script func
    public void ResetCollisionScript()
    {
        //set size var back to start size
        size = startSize;

        //disable skrinking
        SetShrinkState(false);
        //disable player moving to end point
        SetMovePlayerToEndPointState(false);
    }

    //set shrink state func
    public void SetShrinkState(bool state)
    {
        shrinkEnabled = state;

        if(!state) playerScript.gameObject.transform.localScale = new Vector2(startSize, startSize);
    }

    //shrink player
    private void Shrink()
    {
        size -= Time.deltaTime * shrinkSpeed;

        if (size <= 0)
        {
            SetShrinkState(false);
            size = 0;
        }

        playerScript.gameObject.transform.localScale = new Vector2(size, size);
    }

    //set move player to end point state func
    public void SetMovePlayerToEndPointState(bool state)
    {
        isAtEndPoint = state;
    }

    //move player to end point
    private void MovePlayerToEndPoint()
    {
        playerObject.transform.position = Vector3.MoveTowards(playerObject.transform.position, endPoint, endSpeed * Time.deltaTime);
    }

    //check for trigger collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "End" && playerScript.GetCanMove()) // end point triggered
        {
            gameManagerScript.Win();
            ReachedEnd();
        }
    }

    //check for collision
    private void OnCollisionEnter2D()
    {
        if (!collision) return;

        //explosion particle effect
        Explode();

        //turn timer off
        timerManagerScript.SetTimerState(false);

        //call win function in game manager
        gameManagerScript.Lose();

        //hide bottle by disabling this gameobject
        gameObject.SetActive(false);
    }

    //explode func
    private void Explode()
    {
        //set explosion position
        explosion.gameObject.transform.position = transform.position;
        //activate explosion
        explosion.gameObject.SetActive(true);
    }

    //reached end func
    private void ReachedEnd()
    {
        //disable bottle collider
        GetComponent<BoxCollider2D>().enabled = false;

        //start shrink
        SetShrinkState(true);
        //start moving player to start
        SetMovePlayerToEndPointState(true);
    }
}
