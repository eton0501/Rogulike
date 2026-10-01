using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad;
    public GameObject deletePanel;
    public CharactersSelector[] charactersToDelete;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void DeleteSave()
    {
        deletePanel.SetActive(true);
    }

    public void ConfirmDelete()
    {
        deletePanel.SetActive(false);
        foreach(CharactersSelector theChar in charactersToDelete)
        {
            PlayerPrefs.SetInt(theChar.playerToSpawn.name,0);
        }

    }

    public void CancelDelete()
    {
        deletePanel.SetActive(false);
    }
}
