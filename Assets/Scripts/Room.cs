using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Room : MonoBehaviour
{
    public bool closeWhenEntered;
    public GameObject[] doors;
    public bool roomActive;
    public GameObject mapHider;
    

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void OpenDoors()
    {
        foreach(GameObject door in doors)
        {
            door.SetActive(false);
            closeWhenEntered=false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            CameraController.instance.ChangeTarget(transform);
            if (closeWhenEntered)
            {
                foreach(GameObject door in doors)
                {
                    door.SetActive(true);
                }
            }
            mapHider.SetActive(false);
        }
    }
}
