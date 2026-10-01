using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    public float waitToPress=2f;
    public GameObject pressAnyKeyText;
    public string mainMenuScene;

    void Start()
    {
        Time.timeScale=1f;
        Destroy(PlayerController.instance.gameObject);
    }

    
    void Update()
    {
        if (waitToPress > 0)
        {
            waitToPress-=Time.deltaTime;
            if (waitToPress <= 0)
            {
                pressAnyKeyText.SetActive(true);
            }
        }
        else
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene(mainMenuScene);
            }
        }
    }
}
