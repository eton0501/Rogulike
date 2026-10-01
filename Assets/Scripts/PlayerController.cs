using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using System.ComponentModel;
using UnityEngine.Rendering.Universal;
using UnityEditor.Timeline;
using System.Net.Security;
using System.Reflection.Emit;


public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;//新增單例
    public float moveSpeed;//玩家移動速度
    public Rigidbody2D rigidbody;//玩家剛體
    private Vector2 moveInput;//接收玩家輸入
    public Transform gunArm;//槍的Transform
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    private float activeMoveSpeed;
    public float dashSpeed,dashLength,dashCoolDown,dashInvincibility;
    public float dashCounter,dashCoolCounter;
    public bool canMove=true;
    public List<Gun> availableGuns=new List<Gun>();
    public int currentGun;
    public Transform startPoint;



    void Awake()
    {
        instance=this;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        activeMoveSpeed=moveSpeed;
        dashCounter=0;
        dashCoolCounter=0;
        UIContorller.instance.gunImage.sprite=availableGuns[currentGun].gunImage;
        UIContorller.instance.gunText.text=availableGuns[currentGun].gunName;
    }

    
    void Update()
    {

        if (canMove&&!LevelManager.instance.isPaused)
        {
            GetPlayerMoveInput();
            PlayerMove();

            Vector3 mousePos = Input.mousePosition;
            Vector3 screenPoint = CameraController.instance.mainCamera.WorldToScreenPoint(transform.position);

            ChangeGunDirection(mousePos, screenPoint);
            ChangeGunRotation(mousePos, screenPoint);

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (availableGuns.Count > 0)
                {
                    currentGun++;
                    if (currentGun >= availableGuns.Count)
                    {
                        currentGun=0;
                    }
                    SwitchGun();
                }
            }

            if (dashCoolCounter > 0)
            {
                dashCoolCounter -= Time.deltaTime;
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Dash();
                }
            }
            if (dashCounter > 0)
            {
                dashCounter -= Time.deltaTime;
            }
            else
            {
                activeMoveSpeed = moveSpeed;
            }

            if (moveInput != Vector2.zero)
            {
                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
        }
        else
        {
            rigidbody.linearVelocity=Vector2.zero;
            animator.SetBool("isWalking",false);
        }   
    }

    private void Dash()
    {
        activeMoveSpeed = dashSpeed;
        dashCoolCounter = dashCoolDown;
        dashCounter = dashLength;
        animator.SetTrigger("Dash");
        PlayerHealthController.instance.MakeInvincible(dashInvincibility);
        AudioManager.instance.PlaySFX(8);
    }


    private void ChangeGunRotation(Vector3 mousePos, Vector3 screenPoint)
    {
        Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        gunArm.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void ChangeGunDirection(Vector3 mousePos, Vector3 screenPoint)
    {
        if (mousePos.x < screenPoint.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            gunArm.localScale = new Vector3(-1f, -1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(1f, 2f, 3f);
            transform.localScale = Vector3.one;
            gunArm.localScale = Vector3.one;
        }
    }

    private void PlayerMove()
    {
        rigidbody.linearVelocity = moveInput * activeMoveSpeed;
    }

    void GetPlayerMoveInput()
    {
       moveInput.x=Input.GetAxisRaw("Horizontal");
       moveInput.y=Input.GetAxisRaw("Vertical"); 
    }

    public void SwitchGun()
    {
        foreach(Gun theGun in availableGuns)
        {
            theGun.gameObject.SetActive(false);
        }
        availableGuns[currentGun].gameObject.SetActive(true);
        UIContorller.instance.gunImage.sprite=availableGuns[currentGun].gunImage;
        UIContorller.instance.gunText.text=availableGuns[currentGun].gunName;
    }
}
