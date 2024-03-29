using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyAttack : MonoBehaviour
{
    private Enemy enemy;
    [SerializeField] private float cooldown;
    public float currentCooldown;
    private bool isAttacking = false;
    public float attackDuration;
    private float currentAttackTime;
    public GameObject attackIndicator;
    private bool isDone = false;



    private void Start()
    {
        currentCooldown = cooldown;
        enemy = GetComponent<Enemy>();
        enemy.enemyAttack.attackDuration = 5f / (enemy.weaponHolder.baseWeapon.speed + enemy.weaponHolder.headWeapon.speed);
    }
    
    private void Update()
    {
        if (isAttacking) 
        {
            currentAttackTime -= Time.deltaTime;
        }
        else
        {
            currentCooldown -= Time.deltaTime;
        }
        if (currentAttackTime <= 0)
        {
            isAttacking = false;
            currentCooldown += currentAttackTime;
        }
}

    public void attackIndicatore()
    {
        if (!isDone && currentCooldown < 0)
        {
            Instantiate(attackIndicator, transform.position, transform.rotation);
            Invoke("AttackOrCooldown", 1f);
            isDone = true;
        }
    }
    public void AttackOrCooldown()
    {
        if (currentCooldown <= 0 && !isAttacking)
        {
            currentCooldown = cooldown;
            currentAttackTime = attackDuration;
            Attack();
            isDone = false;
        }
      
    }

    virtual protected void Attack() 
    {
        
        if (!isAttacking)
        {
            isAttacking = true;
            currentAttackTime = attackDuration;
            enemy.animator.SetTrigger("Attack");
            SoundManager.PlaySound(SoundManager.Sound.NormalAttack);
           
        }
    }

}
