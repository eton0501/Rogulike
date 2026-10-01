using UnityEngine;

public class SpriteSortOrder : MonoBehaviour
{
    private SpriteRenderer theSR;
    void Start()
    {
        theSR=GetComponent<SpriteRenderer>();
        theSR.sortingOrder=Mathf.RoundToInt(transform.position.y*-10f);
    }

   
    void Update()
    {
        
    }
}
