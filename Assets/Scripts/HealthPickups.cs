using UnityEngine;

public class HealthPickups : MonoBehaviour
{
    public int healAmount;
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
            PlayerHealthController.instance.HealthPlayer(healAmount);
            Destroy(gameObject);
            AudioManager.instance.PlaySFX(7);
        }
    }
}
