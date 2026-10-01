using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    public string levelToLoad;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            PlayerController.instance.canMove=false;
            StartCoroutine(LevelManager.instance.LevelEnd());
        }
    }
}
