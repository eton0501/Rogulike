using UnityEngine;

public class GunPickUp : MonoBehaviour
{
    public float waitToPickup=0.5f;
    public Gun theGun;

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
            bool hasGun=false;
            foreach(Gun gunToCheck in PlayerController.instance.availableGuns)
            {
                if(theGun.gunName == gunToCheck.gunName)
                {
                    hasGun=true;
                }
            }
            if (!hasGun)
            {
                Gun gunClone=Instantiate(theGun);
                gunClone.transform.parent=PlayerController.instance.gunArm;
                gunClone.transform.position=PlayerController.instance.gunArm.transform.position;
                gunClone.transform.localRotation=Quaternion.Euler(Vector3.zero);
                gunClone.transform.localScale=Vector3.one;

                PlayerController.instance.availableGuns.Add(gunClone);
                PlayerController.instance.currentGun=PlayerController.instance.availableGuns.Count-1;
                PlayerController.instance.SwitchGun();
            }
            Destroy(gameObject);
            AudioManager.instance.PlaySFX(7);
        }
    }
}
