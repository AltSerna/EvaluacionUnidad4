using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstEnemy : MonoBehaviour
{
    public int health;
    private GameObject player;
    PlayerHealth scriptPlayerHealth;
    PlayerMovement ScriptMovementPlayer;
    StateMachinePlayer stateMachine;
    public GameObject coins;
    private float CoinsAmmount;
    static int enemiesKilled=0;

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        player = GameObject.FindGameObjectWithTag("Player");
        scriptPlayerHealth = player.GetComponent<PlayerHealth>();
        ScriptMovementPlayer = player.GetComponent<PlayerMovement>();
        stateMachine = player.GetComponent<StateMachinePlayer>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("just attacked a son of a bitch");

        if (health<=0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
        CoinsAmmount = UnityEngine.Random.Range(2, 6);
        enemiesKilled++;
        while (CoinsAmmount >= 0)
        {
            ObjectPooler.Instance.SpawnFromPool("coins", gameObject.transform); 
            CoinsAmmount--;
        }
    }

    private void OnTriggerEnter2D(Collider2D coli)
    {
        if (ScriptMovementPlayer.attackable == true && coli.gameObject.CompareTag("Player"))
        {
            stateMachine.state=2;
            scriptPlayerHealth.LoseHealth();
            ScriptMovementPlayer.knockbackCounter = 0.32f;
            ScriptMovementPlayer.alreadyDashed = false;
        }
    }
}
