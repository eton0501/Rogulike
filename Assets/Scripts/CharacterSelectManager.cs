using UnityEngine;

public class CharacterSelectManager : MonoBehaviour
{
    public static CharacterSelectManager instance;
    public CharactersSelector activeCharSelect;
    public PlayerController activePlayer;

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
}
