using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerHealthController.instance.DamagePlayer();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerHealthController.instance.DamagePlayer();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerHealthController.instance.DamagePlayer();
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerHealthController.instance.DamagePlayer();
        }
    }
}
