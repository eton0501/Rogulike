using System.Threading;
using UnityEngine;

public class GunChest : MonoBehaviour
{
    public GunPickUp[] availablePickUpGuns;
    public GameObject notification;
    public SpriteRenderer theSR;
    public Sprite chestOpen;
    private bool canOpen,isOpen;
    public Transform spawnPoint;
    public float scaleSpeed;

    void Start()
    {
        isOpen=false;
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canOpen && !isOpen)
        {
            int gunSlect=Random.Range(0,availablePickUpGuns.Length);
            Instantiate(availablePickUpGuns[gunSlect],spawnPoint.position,spawnPoint.rotation);
            theSR.sprite=chestOpen;
            isOpen=true;
            transform.localScale=new Vector3(1.2f,1.2f,1.2f);
        }

        if (isOpen)
        {
            transform.localScale=Vector3.MoveTowards(transform.localScale,Vector3.one,Time.deltaTime*scaleSpeed);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            notification.SetActive(true);
            canOpen=true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            notification.SetActive(false);
            canOpen=false;
        }
    }
}
