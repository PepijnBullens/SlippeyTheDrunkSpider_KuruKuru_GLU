using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBottleScript : MonoBehaviour
{
    [SerializeField]
    private PlayerScript playerScript;

    private GameObject pickUpBottle;

    private void Start()
    {
        pickUpBottle = transform.GetChild(0).gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerScript.SetBottleState(true);
        SetPickUpBottleState(false);
    }

    public void SetPickUpBottleState(bool state)
    {
        pickUpBottle.SetActive(state);
    }
}
