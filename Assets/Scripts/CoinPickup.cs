using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int coinValue=1;
    public float waitToPickup=0.5f;
    void Start()
    {
        
    }

    
    void Update()
    {
        if (waitToPickup > 0)
        {
            waitToPickup-=Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && waitToPickup<=0)
        {
            LevelManager.instance.GetCoins(coinValue);
            Destroy(gameObject);
            AudioManager.instance.PlaySFX(5);
        }
    }
}
