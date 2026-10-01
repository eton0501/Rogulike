using UnityEngine;

public class CharactersSelector : MonoBehaviour
{
    public GameObject message;
    public bool canSelect;
    public PlayerController playerToSpawn;

    void Start()
    {
        if (PlayerPrefs.HasKey(playerToSpawn.name))
        {
            if (PlayerPrefs.GetInt(playerToSpawn.name) == 1)
            {
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (canSelect)
            {
                Vector3 playerPos=PlayerController.instance.transform.position;
                Destroy(PlayerController.instance.gameObject);
                PlayerController newPlayer=Instantiate(playerToSpawn,playerPos,playerToSpawn.transform.rotation);
                gameObject.SetActive(false);
                PlayerController.instance=newPlayer;
                CameraController.instance.target=newPlayer.transform;
                CharacterSelectManager.instance.activePlayer=newPlayer;
                CharacterSelectManager.instance.activeCharSelect.gameObject.SetActive(true);
                CharacterSelectManager.instance.activeCharSelect=this;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canSelect=true;
            message.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canSelect=false;
            message.SetActive(false);
        }
    }
}
