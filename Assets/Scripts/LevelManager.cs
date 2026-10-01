using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public string nextLevel;
    public float waitToLoad=4f;
    public bool isPaused=false;
    public int currentCoins;
    public Transform startPoint;

    void Awake()
    {
        instance=this;
    }
    void Start()
    {
        PlayerController.instance.transform.position=startPoint.position;
        PlayerController.instance.canMove=true;
        currentCoins=CharacterTracker.instance.currentCoins;
        UIContorller.instance.coinText.text=currentCoins.ToString();
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    public IEnumerator LevelEnd()
    {
        AudioManager.instance.PlayLevelWin();
        UIContorller.instance.StartFadeToBlack();
        yield return new WaitForSeconds(waitToLoad);
        CharacterTracker.instance.currentHealth=PlayerHealthController.instance.currentHealth;
        CharacterTracker.instance.maxHealth=PlayerHealthController.instance.maxHealth;
        CharacterTracker.instance.currentCoins=currentCoins;
        SceneManager.LoadScene(nextLevel);
    }

    public void PauseUnpause()
    {
        if (!isPaused)
        {
            UIContorller.instance.PauseScreen.SetActive(true);
            Time.timeScale=0f;
            isPaused=true;
        }
        else
        {
            UIContorller.instance.PauseScreen.SetActive(false);
            Time.timeScale=1f;
            isPaused=false;
        }
    }

    public void GetCoins(int amount)
    {
        currentCoins+=amount;
        UIContorller.instance.coinText.text=currentCoins.ToString();
    }

    public void SpendCoins(int amount)
    {
        currentCoins-=amount;
        if (currentCoins < 0)
        {
            currentCoins=0;
        }
        UIContorller.instance.coinText.text=currentCoins.ToString();
    }
}
