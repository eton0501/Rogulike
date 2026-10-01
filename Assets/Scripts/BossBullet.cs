using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    void Start()
    {
        direction=transform.right;
    }

  
    void Update()
    {   
        transform.position+=direction*speed*Time.deltaTime;
        if (!BossController.instance.gameObject.activeInHierarchy)
        {
            Destroy(gameObject);
        }
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
