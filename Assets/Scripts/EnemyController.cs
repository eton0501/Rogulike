using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    public float moveSpeed;

    [Header("Chase Player")]
    public bool shouldChasePlayer;
    public float rangeToChasePlayer;
    private Vector3 moveDirection;

    [Header("Run Away")]
    public float runawayRange;
    public bool shouldRunAway;

    [Header("Wandering")]
    public bool shouldWander;
    public float wanderCounter,pauseCounter;
    public float wanderLength,pauseLength;
    public Vector3 wanderDirection;

    [Header("Patrolling")]
    public bool shouldPatrol;
    public Transform[] patrolPoints;
    public int currentPatrolPoint;

    [Header("Shooting")]
    public bool shouldShoot;
    public GameObject bullet;
    public Transform firePoint;
    public float fireRate;
    private float fireCounter;
    public float fireRange;

    [Header("Variables")]
    public Animator animator;
    public int health;
    public GameObject[] deathSplatters;
    public GameObject hitEffect;
    public SpriteRenderer theBody;

    [Header("DropItems")]
    public bool shouldDropItems;
    public GameObject[] itemsToDrop;
    public float dropPercent;
    
    void Start()
    {
        fireCounter=fireRate;
        if (pauseCounter == 0)
        {
            wanderCounter=Random.Range(wanderLength*0.75f,wanderLength*1.25f);
        }
    }

   
    void Update()
    {
        if (theBody.isVisible&&PlayerController.instance.gameObject.activeInHierarchy)
        {
            moveDirection=Vector3.zero;
            if (Vector3.Distance(transform.position,PlayerController.instance.transform.position) < rangeToChasePlayer && shouldChasePlayer)
            {
                moveDirection=PlayerController.instance.transform.position-transform.position;
            }
            else
            {
                if (shouldWander)
                {
                    if (wanderCounter > 0)
                    {
                        wanderCounter-=Time.deltaTime;
                        moveDirection=wanderDirection;
                        if (wanderCounter <= 0)
                        {
                            pauseCounter=Random.Range(pauseLength*0.75f,pauseLength*1.25f);
                        }
                    }
                    if (pauseCounter > 0)
                    {
                        pauseCounter-=Time.deltaTime;
                        moveDirection=Vector3.zero;
                        if (pauseCounter <= 0)
                        {
                            wanderCounter=Random.Range(wanderLength*0.75f,wanderLength*1.25f);
                            wanderDirection=new Vector3(Random.Range(-1f,1f),Random.Range(-1f,1f),0f);
                        }
                    }
                }
                if (shouldPatrol)
                {
                    moveDirection=patrolPoints[currentPatrolPoint].position-transform.position;
                    if (Vector3.Distance(patrolPoints[currentPatrolPoint].position, transform.position) < 0.2f)
                    {
                        currentPatrolPoint++;
                        if (currentPatrolPoint >= patrolPoints.Length)
                        {
                            currentPatrolPoint=0;
                        }
                    }
                }
            }
            if(Vector3.Distance(transform.position,PlayerController.instance.transform.position) < rangeToChasePlayer && shouldRunAway)
            {
                moveDirection=transform.position-PlayerController.instance.transform.position;
            }
            moveDirection.Normalize();
            rigidbody2D.linearVelocity=moveDirection*moveSpeed;

            if (shouldShoot&&Vector3.Distance(PlayerController.instance.transform.position,transform.position)<fireRange)
            {
                fireCounter-=Time.deltaTime;
                if (fireCounter <= 0)
                {
                    Instantiate(bullet,firePoint.position,firePoint.rotation);
                    AudioManager.instance.PlaySFX(13);
                    fireCounter=fireRate;
                }
            }

            if (moveDirection != Vector3.zero)
            {  
                animator.SetBool("isWalking",true);
            }
            else
            {   
                animator.SetBool("isWalking",false);
            }  
        }
        else
        {  
            rigidbody2D.linearVelocity=Vector3.zero;
            animator.SetBool("isWalking",false);
        }
    }

    public void DamageEnemy(int damage)
    {
        health-=damage;
        Instantiate(hitEffect,transform.position,transform.rotation);
        AudioManager.instance.PlaySFX(2);
        if (health <= 0)
        {
            Destroy(gameObject);
            AudioManager.instance.PlaySFX(1);
            int slectedSpllater=Random.Range(0,deathSplatters.Length);
            int rotation=Random.Range(0,4);
            Instantiate(deathSplatters[slectedSpllater],transform.position,Quaternion.Euler(0f,0f,rotation*90f));
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
    }
}
