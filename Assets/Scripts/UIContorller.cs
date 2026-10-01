using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIContorller : MonoBehaviour
{
    public static UIContorller instance;
    public Slider healthSlider;
    public Text healthText,coinText;
    public GameObject deathScreen,PauseScreen;
    public Image fadeScreen;
    public float fadeSpeed;
    private bool fadeToBlack,fadeOutBlack;
    public string mainMenu,Restart;
    public GameObject mapImage,bigMapText;
    public Image gunImage;
    public Text gunText;
    public Slider bossHealthBar;
    void Awake()
    {
        instance=this;
    }
    void Start()
    {
        fadeToBlack=false;
        fadeOutBlack=true;
        gunImage.sprite=PlayerController.instance.availableGuns[PlayerController.instance.currentGun].gunImage;
        gunText.text=PlayerController.instance.availableGuns[PlayerController.instance.currentGun].gunName;
    }

    
    void Update()
    {
        if (fadeOutBlack)
        {
            fadeScreen.color=new Color(fadeScreen.color.r,fadeScreen.color.g,fadeScreen.color.b,Mathf.MoveTowards(fadeScreen.color.a,0f,fadeSpeed*Time.deltaTime));
            if (fadeScreen.color.a == 0f)
            {
                fadeOutBlack=false;
            }
        }
        if (fadeToBlack)
        {
            fadeScreen.color=new Color(fadeScreen.color.r,fadeScreen.color.g,fadeScreen.color.b,Mathf.MoveTowards(fadeScreen.color.a,1f,fadeSpeed*Time.deltaTime));
            if (fadeScreen.color.a == 1f)
            {
                fadeToBlack=false;
            }
        }
    }

    public void StartFadeToBlack()
    {
        fadeToBlack=true;
        fadeOutBlack=false;
    }
    public void RestartGame()
    {
        Time.timeScale=1f;
        SceneManager.LoadScene(Restart);
        Destroy(PlayerController.instance.gameObject);
    }
    public void MainMenu()
    {
        Time.timeScale=1f;
        SceneManager.LoadScene(mainMenu);
        Destroy(PlayerController.instance.gameObject);
    }
    public void Resume()
    {
        PauseScreen.SetActive(false);
        Time.timeScale=1f;
        LevelManager.instance.isPaused=false;
    }
}
