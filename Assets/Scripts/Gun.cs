using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletToFire;//發射的子彈
    public Transform firePoint;//子彈發射出去的點
    public float timeBetweenFire;//間隔多久射一次
    private float fireCounter;//開槍的計時器
    public string gunName;
    public Sprite gunImage;
    public Sprite buyGunImage;
    public int gunCost;
    
    void Start()
    {
        
    }

    void Update()
    {
        if(PlayerController.instance.canMove && !LevelManager.instance.isPaused)
        {
            if (fireCounter > 0)
            {
                fireCounter-=Time.deltaTime;
            }
            else
            {
                if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
                {
                    Fire();
                }
            }
        }
    }

    private void Fire()
    {
        Instantiate(bulletToFire, firePoint.position, firePoint.rotation);//生成子彈預製體
        AudioManager.instance.PlaySFX(12);
        fireCounter = timeBetweenFire;
    }
}
