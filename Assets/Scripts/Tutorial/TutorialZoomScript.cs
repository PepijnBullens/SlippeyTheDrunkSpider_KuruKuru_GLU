using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialZoomScript : MonoBehaviour
{
    [Header ("Scaling Speed")]

    [SerializeField]
    private float zoomOutSpeed;
    [SerializeField]
    private float[] sizeScalings;

    //camera reference
    private Camera camera;
    //keep track of when to zoom in and out
    private bool zoomOut;

    //start
    private void Start()
    {
        camera = GameObject.Find("MainCamera").GetComponent<Camera>();
    }

    //when entering tutorial zone
    private void OnTriggerEnter2D(Collider2D collision)
    {
        zoomOut = true;
    }

    //when exiting tutorial zone
    private void OnTriggerExit2D(Collider2D collision)
    {
        zoomOut = false;
    }

    //update
    private void Update()
    {
        if(zoomOut)
        {
            if (camera.orthographicSize < sizeScalings[1])
            {
                camera.orthographicSize += zoomOutSpeed * Time.deltaTime;
            }
        }
        else
        {
            if (camera.orthographicSize > sizeScalings[0])
            {
                camera.orthographicSize -= zoomOutSpeed * Time.deltaTime;
            }
        }
    }
}
