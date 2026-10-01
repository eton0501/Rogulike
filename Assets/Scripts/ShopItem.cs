using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public GameObject buyPurchaseText;
    public bool inBuyZone;
    public int itemCost;
    public bool isHealthRestore,isUpgradeHealth,isWeapon;
    public int healthUpgradeAmount;
    public SpriteRenderer gunSprite;
    public Text gunText;
    public Gun[] potentialGuns;
    public Gun theGun;
    void Start()
    {
        if (isWeapon)
        {
            int selectGun=Random.Range(0,potentialGuns.Length);
            theGun=potentialGuns[selectGun];
            gunText.text=theGun.gunName+"\n - "+theGun.gunCost+" Gold - ";
            gunSprite.sprite=theGun.buyGunImage;
            itemCost=theGun.gunCost;
        }
    }

    
    void Update()
    {
        if (inBuyZone)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (LevelManager.instance.currentCoins >= itemCost)
                {
                    LevelManager.instance.SpendCoins(itemCost);
                    if (isHealthRestore)
                    {
                        PlayerHealthController.instance.HealthPlayer(PlayerHealthController.instance.maxHealth);
                    }
                    if (isUpgradeHealth)
                    {
                        PlayerHealthController.instance.IncreaseMaxHealth(healthUpgradeAmount);
                    }
                    if (isWeapon)
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
                    AudioManager.instance.PlaySFX(18);
                    gameObject.SetActive(false);
                    inBuyZone=false;
                }
                else
                {
                    AudioManager.instance.PlaySFX(19);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            buyPurchaseText.SetActive(true);
            inBuyZone=true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            buyPurchaseText.SetActive(false);
            inBuyZone=false;
        }
    }
}
