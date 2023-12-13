using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManagerScript : MonoBehaviour
{
    [Header("Player Reference")]

    [SerializeField]
    private GameObject player;

    //positon where player started
    private Vector2 startPosition;

    //position to reset to
    private Vector2 resetPos;

    //start
    private void Start()
    {
        //set reset pos to players current position
        startPosition = player.transform.position;
        //set reset pos to start position
        resetPos = startPosition;
    }

    //reset to start position func
    public void ResetPositionToStart()
    {
        resetPos = startPosition;
    }

    //set reset pos func
    public void SetResetPosition(Vector2 pos)
    {
        resetPos = pos;
    }

    //get reset pos func
    public Vector2 GetResetPosition()
    {
        return resetPos;
    }
}
