using UnityEngine;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource levelMusic,gameOverMusic,winMusic;
    public AudioSource[] sfx;

    void Awake()
    {
        instance=this;
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void PlayGameOver()
    {
        levelMusic.Stop();
        gameOverMusic.Play();
    }

    public void PlayLevelWin()
    {
        levelMusic.Stop();
        gameOverMusic.Play();
    }

    public void PlaySFX(int sfxToPlay)
    {
        sfx[sfxToPlay].Stop();
        sfx[sfxToPlay].Play();
    }

}
