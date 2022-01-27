using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float curHp, maxHp;

    public string enemyName;

    public bool isDead;

    public float respawnTime = 5f;

    public GameObject respawnPoint;

    void Start()
    {

    }

    void Update()
    {
        if (curHp <= 0 && !isDead)
        {
            isDead = true;
            curHp = 0;
            StartCoroutine(Death());
            GameObject.FindGameObjectWithTag("GameController").GetComponent<CombatEvents>().audioSource.Play();
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

    IEnumerator Death()
    {
        yield return new WaitForSeconds(respawnTime);
        Debug.Log("Enemy destroyed!");
        Destroy(gameObject);
    }
}
