using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float curHp, maxHp;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (curHp <= 0)
        {
            Debug.Log("Enemy destroyed!");
            curHp = 0;
            Destroy(gameObject);
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
}
