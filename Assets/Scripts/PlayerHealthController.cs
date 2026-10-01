using System;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    public int maxHealth;
    public int currentHealth;
    public float damageInvincLength=1f;
    public float invincCount;
    void Awake()
    {
        instance=this;
    }
    void Start()
    {
        currentHealth=CharacterTracker.instance.currentHealth;
        maxHealth=CharacterTracker.instance.maxHealth;
        invincCount=0;
        UIContorller.instance.healthSlider.maxValue=maxHealth;
        UIContorller.instance.healthSlider.value=currentHealth;
        UIContorller.instance.healthText.text=currentHealth.ToString()+" / "+maxHealth.ToString();
    }

    
    void Update()
    {
        if (invincCount > 0)
        {
            PlayerController.instance.spriteRenderer.color=new Color(PlayerController.instance.spriteRenderer.color.r,PlayerController.instance.spriteRenderer.color.g,PlayerController.instance.spriteRenderer.color.b,0.5f);
            invincCount-=Time.deltaTime;
        }
        else
        {
            PlayerController.instance.spriteRenderer.color=new Color(PlayerController.instance.spriteRenderer.color.r,PlayerController.instance.spriteRenderer.color.g,PlayerController.instance.spriteRenderer.color.b,1f);
        }
    }

    public void DamagePlayer()
    {
        if (invincCount <= 0)
        {
            currentHealth--;
            invincCount=damageInvincLength;
            AudioManager.instance.PlaySFX(11);
            if (currentHealth <= 0)
            {
                PlayerController.instance.gameObject.SetActive(false);
                UIContorller.instance.deathScreen.SetActive(true);
                AudioManager.instance.PlayGameOver();
                AudioManager.instance.PlaySFX(9);
            }
            UIContorller.instance.healthSlider.value=currentHealth;
            UIContorller.instance.healthText.text=currentHealth.ToString()+"/"+maxHealth.ToString();
        }
    }

    public void MakeInvincible(float length)
    {
        invincCount=length;
    }

    public void HealthPlayer(int healthAmount)
    {
        currentHealth+=healthAmount;
        if (currentHealth >= maxHealth)
        {
            currentHealth=maxHealth;
        }
        UIContorller.instance.healthSlider.value=currentHealth;
        UIContorller.instance.healthText.text=currentHealth.ToString()+"/"+maxHealth.ToString();
    }

    public void IncreaseMaxHealth(int Amount)
    {
        maxHealth+=Amount;
        currentHealth=maxHealth;
        UIContorller.instance.healthSlider.maxValue=maxHealth;
        UIContorller.instance.healthSlider.value=currentHealth;
        UIContorller.instance.healthText.text=currentHealth.ToString()+"/"+maxHealth.ToString();
    }
}
