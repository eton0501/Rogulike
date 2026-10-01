using UnityEngine;

public class CharacterCage : MonoBehaviour
{
    public GameObject message;
    public bool canUnlock;
    public CharactersSelector[] charSelects;
    public CharactersSelector playerToUnlock;
    public SpriteRenderer charSR;

    void Start()
    {
        playerToUnlock=charSelects[Random.Range(0,charSelects.Length)];
        charSR.sprite=playerToUnlock.playerToSpawn.spriteRenderer.sprite;
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (canUnlock)
            {
                PlayerPrefs.SetInt(playerToUnlock.playerToSpawn.name,1);
                Instantiate(playerToUnlock,transform.position,transform.rotation);
                gameObject.SetActive(false);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            message.SetActive(true);
            canUnlock=true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            message.SetActive(false);
            canUnlock=false;
        }
    }
}
