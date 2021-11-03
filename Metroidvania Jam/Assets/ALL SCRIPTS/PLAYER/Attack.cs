using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int PlayerDamage;
    public Transform AttackPoint;
    public Transform UpwardAttack;
    public Transform downwardAttack;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    FirstEnemy ScriptEnemy;
    public bool canAttack;
    StateMachinePlayer statemachine;
    PlayerMovement scriptMovement;
    public float charging = 0.5f;
    public bool powerfullAttacker;
    public float WantPowered = 0.4f;
    private float attackSpeed;
    bool StartCount;
    bool AttackTime;

    public PlayerSO channel;

    private void OnEnable()
    {
        channel.onAttackSide += AttackSide;
        channel.onAttackSide += SideAnimations;
        channel.onAttackSide += AttackSound;

        channel.onAttackUp += AttackUp;
        channel.onAttackUp += UpAnimations;
        channel.onAttackUp += AttackSound;

        channel.onAttackDown += AttackDown;
        channel.onAttackDown += DownAnimations;
        channel.onAttackDown += AttackSound;
    }

    private void OnDisable()
    {
        channel.onAttackSide -= AttackSide;
        channel.onAttackSide -= SideAnimations;
        channel.onAttackSide -= AttackSound;

        channel.onAttackUp -= AttackUp;
        channel.onAttackUp -= UpAnimations;
        channel.onAttackUp -= AttackSound;

        channel.onAttackDown -= AttackDown;
        channel.onAttackDown -= DownAnimations;
        channel.onAttackDown -= AttackSound;
    }

    public void AttackSide()
    {
        StartCoroutine(playerAttackside());
        AttackTime = false;
    }
    public void SideAnimations()
    {
        PlayerMovement.Animator.SetLayerWeight(1, 1);
        PlayerMovement.Animator.SetTrigger("attack");
        PlayerMovement.Animator.SetBool("justAttacked", true);
        Debug.Log("you just attacked");
        StartCount = true;
    }
    public void AttackSound()
    {
        //Implementar
    }

    public void AttackUp()
    {
        StartCoroutine(PlayerAttackUpside());
        AttackTime = false;
    }
    public void UpAnimations()
    {
        PlayerMovement.Animator.SetLayerWeight(1, 1);
        PlayerMovement.Animator.SetTrigger("attackUp");

        Debug.Log("pegaste pa arriba mi loco");
    }

    public void AttackDown()
    {
        StartCoroutine(PlayerAttackDownside());
        AttackTime = false;
    }
    public void DownAnimations()
    {
        PlayerMovement.Animator.SetLayerWeight(1, 1);
        PlayerMovement.Animator.SetTrigger("attackDown");
        Debug.Log("mi loco sos la verga, pegaste pa bajo");
    }


    void Start()
    {
        AttackTime = true;
        PlayerDamage = 20;
        canAttack = true;
        statemachine = GetComponent<StateMachinePlayer>();
        scriptMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (canAttack == true && Input.GetKeyDown(KeyCode.X) && Input.GetKey(KeyCode.UpArrow) && AttackTime==true)
        {
            channel.onUpAttack();
        }
        else if (scriptMovement.grounded == false && canAttack == true && Input.GetKeyDown(KeyCode.X) && Input.GetKey(KeyCode.DownArrow) && AttackTime==true)
        {

            channel.onDownAttack();
        }
        else if (canAttack == true && Input.GetKeyDown(KeyCode.X) && !Input.GetKey(KeyCode.UpArrow) && AttackTime==true)
        {
            channel.onSideAttack();
        }

        if (Input.GetKeyDown(KeyCode.X) && attackSpeed < 1.3f && AttackTime == true)
        {
            PlayerMovement.Animator.SetTrigger("attack");
            PlayerMovement.Animator.SetBool("justAttacked", false);
            attackSpeed = 0;
            StartCount = false;
        }
        else if (attackSpeed>1.3f)
        {
            attackSpeed = 0;
            StartCount = false;
        }

        if (StartCount==true)
        {
            attackSpeed += Time.deltaTime;
        }

        /*
        if (canAttack == true && Input.GetKey(KeyCode.X) && powerfullAttacker == true)
        {
            WantPowered -= Time.deltaTime;
            if (WantPowered<=0)
            {
                PoweredAttack();
            }
            else if (Input.GetKeyUp(KeyCode.X) && WantPowered<0)
            {
                WantPowered = 0.4f;
            }
        }*/
    }


    IEnumerator playerAttackside()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("just hit " + enemy.name);
            if (enemy.tag == "enemie")
            {
                ScriptEnemy = enemy.GetComponent<FirstEnemy>();
                ScriptEnemy.TakeDamage(PlayerDamage);
            }
        }

        yield return new WaitForSeconds(0.43f);
        //Debug.Log("can attack again");
        AttackTime = true;
        PlayerMovement.Animator.SetLayerWeight(1, 0);
    }

    IEnumerator PlayerAttackUpside()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(UpwardAttack.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("just hit " + enemy.name);
            if (enemy.tag == "enemie")
            {
                ScriptEnemy = enemy.GetComponent<FirstEnemy>();
                ScriptEnemy.TakeDamage(PlayerDamage);
            }
        }

        yield return new WaitForSeconds(0.43f);
        //Debug.Log("can attack again");
        AttackTime = true;
        PlayerMovement.Animator.SetLayerWeight(1, 0);
    }

    IEnumerator PlayerAttackDownside()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(downwardAttack.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("just hit " + enemy.name);
            if (enemy.tag == "enemie")
            {
                ScriptEnemy = enemy.GetComponent<FirstEnemy>();
                ScriptEnemy.TakeDamage(PlayerDamage);
                scriptMovement.RgidBody.velocity = Vector2.up * 27.4f;
            }
        }
        yield return new WaitForSeconds(0.43f);
        //Debug.Log("can attack again");
        AttackTime = true;
        PlayerMovement.Animator.SetLayerWeight(1, 0);
    }

    /*public void PlayerAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, attackRange,enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("just hit " + enemy.name);
            if (enemy.tag=="enemie")
            {
                ScriptEnemy = enemy.GetComponent<FirstEnemy>();
                ScriptEnemy.TakeDamage(PlayerDamage);
            }
        }
    }

    public void PlayerUpAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(UpwardAttack.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("just hit " + enemy.name);
            if (enemy.tag == "enemie")
            {
                ScriptEnemy = enemy.GetComponent<FirstEnemy>();
                ScriptEnemy.TakeDamage(PlayerDamage);
            }
        }
    }

    public void PlayerDownAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(downwardAttack.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("just hit " + enemy.name);
            if (enemy.tag == "enemie")
            {
                ScriptEnemy = enemy.GetComponent<FirstEnemy>();
                ScriptEnemy.TakeDamage(PlayerDamage);
                scriptMovement.RgidBody.velocity = Vector2.up * 27.4f;
            }
        }
    }

    /*
    void PoweredAttack()
    {
        
        if (powerfullAttacker==true)
        {
            Debug.Log("poder");
            charging -= Time.deltaTime;
            statemachine.state = 3;
            if (charging<=0 && Input.GetKeyUp(KeyCode.X))
            {
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, attackRange*2.5f, enemyLayers);
                foreach (Collider2D enemy in hitEnemies)
                {
                    Debug.Log("just hit " + enemy.name);
                    if (enemy.tag == "enemie")
                    {
                        ScriptEnemy = enemy.GetComponent<FirstEnemy>();
                        ScriptEnemy.TakeDamage(PlayerDamage*3);
                    }
                }
            }
            else
            {
                charging = 0.5f;
                statemachine.state = 1;
            }
        }
    }*/

    private void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
            return;

        Gizmos.DrawWireSphere(AttackPoint.position, attackRange);

        if (UpwardAttack == null)
            return;

        Gizmos.DrawWireSphere(UpwardAttack.position, attackRange);

        if (downwardAttack == null)
            return;

        Gizmos.DrawWireSphere(downwardAttack.position, attackRange);
    }
}
