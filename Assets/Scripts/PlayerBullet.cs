using System.Text.Json.Serialization.Metadata;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed=7.5f;
    public Rigidbody2D rigidbody2D;
    public GameObject bulletEffect;
    public int damage;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        rigidbody2D.linearVelocity=transform.right*speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(bulletEffect,transform.position,transform.rotation);
        Destroy(gameObject);
        AudioManager.instance.PlaySFX(4);
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyController>().DamageEnemy(damage);
        }
        if (other.tag == "Boss")
        {
            BossController.instance.TakeDamage(damage);
            Instantiate(BossController.instance.hitEffect,transform.position,transform.rotation);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
