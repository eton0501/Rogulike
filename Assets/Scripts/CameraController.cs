using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public Transform target;
    public float moveSpeed;
    public Camera mainCamera,bigMapCamera;
    public bool bigMapActive;
    public bool isBossRoom;

    void Awake()
    {
        instance=this;
    }
    void Start()
    {
        if (isBossRoom)
        {
            target=PlayerController.instance.transform;
        }
    }
    void Update()
    {
        if (target != null)
        {
            transform.position=Vector3.MoveTowards(transform.position,new Vector3(target.position.x,target.position.y,transform.position.z),moveSpeed*Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.M) && !isBossRoom)
        {
            if (!bigMapActive)
            {
                ActivateBigMap();
            }
            else
            {
                DeactivateBigMap();
            }
        }   
    }
    public void ChangeTarget(Transform newtarget)
    {
        target=newtarget;
    }
    public void ActivateBigMap()
    {
        if (!LevelManager.instance.isPaused)
        {
            bigMapActive=true;
            mainCamera.enabled=false;
            bigMapCamera.enabled=true;
            PlayerController.instance.canMove=false;
            Time.timeScale=0f;
            UIContorller.instance.mapImage.SetActive(false);
            UIContorller.instance.bigMapText.SetActive(true);
        }
        
    }
    public void DeactivateBigMap()
    {   
        if (!LevelManager.instance.isPaused)
        {
            bigMapActive=false;
            mainCamera.enabled=true;
            bigMapCamera.enabled=false;
            PlayerController.instance.canMove=true;
            Time.timeScale=1f;
            UIContorller.instance.mapImage.SetActive(true);
            UIContorller.instance.bigMapText.SetActive(false);   
        }
    }
}
