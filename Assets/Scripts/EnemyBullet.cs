using Unity.VisualScripting;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    void Start()
    {
        direction=PlayerController.instance.transform.position-transform.position;
        direction.Normalize();
    }

  
    void Update()
    {   
        transform.position+=direction*speed*Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerHealthController.instance.DamagePlayer();
        }
        Destroy(gameObject);
        AudioManager.instance.PlaySFX(4);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
