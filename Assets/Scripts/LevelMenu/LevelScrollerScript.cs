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

    private int currentPage;
    private RectTransform pagesRectTransform;

    private void Start()
    {
        pagesRectTransform = transform.GetChild(0).gameObject.GetComponent<RectTransform>();
    }

    public void PageForward()
    {
        if(currentPage < numberOfPages) currentPage++;
    }

    public void PageBack() 
    {
        if (currentPage > 0) currentPage--;
    }

    private void Update()
    {
        float pagesX = pagesRectTransform.anchoredPosition.x;

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
