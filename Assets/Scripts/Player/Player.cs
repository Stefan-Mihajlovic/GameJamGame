using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    public Animator animator;
    public Animator spriteAnimator;
    public PlayerAttack playerAttack;

    [SerializeField] private GameObject boom;
     public WeaponHolder weaponHolder;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        animator = gameObject.GetComponent<Animator>();
        spriteAnimator = transform.Find("Gfx").GetComponent<Animator>();
        playerAttack = GetComponent<PlayerAttack>();
        animator.runtimeAnimatorController = weaponHolder.baseWeapon.animator;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            playerAttack.Attack();
        }
        if(currentHealth<=0)
        {
            boom.SetActive(true);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        healthBar.SetHealth(currentHealth);
    }

    public void Heal(int amount)
    {
        currentHealth += amount;

        if(currentHealth > maxHealth) 
        {
            currentHealth = maxHealth;
        }
        healthBar.SetHealth(currentHealth);
    }

    public void SetNewPlayerItem(WeaponItem newItem)
    {
        if(newItem.isBase)
        {
            weaponHolder.baseWeapon = newItem;
            animator.runtimeAnimatorController = newItem.animator;
        }
        else
        {
            weaponHolder.headWeapon = newItem;
        }
        weaponHolder.weaponImages.UpdateImages();
        weaponHolder.UpdateWeapon();
        playerAttack.attackDuration = 5f / (weaponHolder.baseWeapon.speed + weaponHolder.headWeapon.speed);
    }
}
