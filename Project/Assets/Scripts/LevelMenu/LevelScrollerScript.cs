using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScrollerScript : MonoBehaviour
{
    [SerializeField]
    private int numberOfPages;
    [SerializeField]
    private float pageWidth = 600f;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float tolerance;

    //keep track of what page we are currently on
    private int currentPage;
    //reference to the rect transform of the object we want to move
    private RectTransform pagesRectTransform;

    //start
    private void Start()
    {
        //set pagesRectTransform to the rect transform of this gameobjects child
        pagesRectTransform = transform.GetChild(0).gameObject.GetComponent<RectTransform>();
    }

    //go to next page func
    public void PageForward()
    {
        if(currentPage < numberOfPages) currentPage++;
    }

    //go back page func
    public void PageBack() 
    {
        if (currentPage > 0) currentPage--;
    }

    //update
    private void Update()
    {
        //get current x position of pagesRectTransform
        float pagesX = pagesRectTransform.anchoredPosition.x;


        /* ---- if pagesX is not at desired position. Move towards that position ---- */

        if(pagesX < (pageWidth * -currentPage) - tolerance)
        {
            pagesRectTransform.anchoredPosition += new Vector2(moveSpeed * Time.deltaTime, 0f);
        }
        
        if(pagesX > (pageWidth * -currentPage) + tolerance)
        {
            pagesRectTransform.anchoredPosition -= new Vector2(moveSpeed * Time.deltaTime, 0f); 
        }
    }
}
