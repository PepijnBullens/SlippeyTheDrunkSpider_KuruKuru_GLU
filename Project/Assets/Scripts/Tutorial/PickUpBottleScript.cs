using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBottleScript : MonoBehaviour
{
    [SerializeField]
    private PlayerScript playerScript;

    //reference to pick up bottle
    private GameObject pickUpBottle;

    //start
    private void Start()
    {
        //set pickUpBottle variable to the first child of this gameobject
        pickUpBottle = transform.GetChild(0).gameObject;
    }

    //check if player picked up pick up bottle
    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerScript.SetBottleState(true);
        SetPickUpBottleState(false);
    }

    //set pick up bottle inactive or active 
    public void SetPickUpBottleState(bool state)
    {
        pickUpBottle.SetActive(state);
    }
}
