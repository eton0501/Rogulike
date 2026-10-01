using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Breakables : MonoBehaviour
{
    public GameObject[] brokenPieces;
    public int maxPiece;
    public bool shouldDropItems;
    public GameObject[] itemsToDrop;
    public float dropPercent;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void Smash()
    {
        Destroy(gameObject);
        AudioManager.instance.PlaySFX(0);
        for(int i = 0; i < maxPiece; i++)
        {
            int slectedBroken=Random.Range(0,brokenPieces.Length);
            Instantiate(brokenPieces[slectedBroken],transform.position,transform.rotation);   
        }
        if (shouldDropItems)
        {
            float dropChance=Random.Range(0f,100f);
            if (dropChance <= dropPercent)
            {
                int dropItemsIndex=Random.Range(0,itemsToDrop.Length);
                Instantiate(itemsToDrop[dropItemsIndex],transform.position,transform.rotation);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (PlayerController.instance.dashCounter > 0)
            {
                Smash();         
            }   
        }
        if (other.tag == "PlayerBullet")
        {
            Smash();
        }
    }
}
