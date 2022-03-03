using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float curHp, maxHp;

    public string enemyName;

    public bool isDead;

    public float respawnTime;

    public GameObject respawnPoint;

    public Respawn respawn;

    void Start()
    {
        respawnTime = 5f;
        respawnPoint = GameObject.Find("StartHumanRespawnPoint");
        respawn = respawnPoint.GetComponent<Respawn>();
    }

    void Update()
    {
        if (curHp <= 0 && !isDead)
        {
            isDead = true;
            curHp = 0;
            Death();
        }
    }

    public void ReceiveDamage(float dmg)
    {
        if (curHp > 0)
        {
            curHp -= dmg;
            Debug.Log("Enemy received " + dmg + " damage.");
            Debug.Log("Enemy HP after receiving dmg: " + curHp);
        }
    }

    private void Death()
    {
        //StartCoroutine(respawn.Spawn(respawnTime, gameObject));
        respawn.StartCoroutine(respawn.Spawn(respawnTime));
        Destroy(this.gameObject);
        GameObject.FindGameObjectWithTag("GameController").GetComponent<CombatEvents>().audioSource.Play();
        //respawn.Spawn();
    }
}
