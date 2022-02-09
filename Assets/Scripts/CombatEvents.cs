using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CombatEvents : MonoBehaviour
{
    public AudioSource audioSource;

    public delegate void EnemyEventHandler(IEnemy enemy);
    public static event EnemyEventHandler OnEnemyDeath;

    public static void EnemyDied(IEnemy enemy)
    {
        if (OnEnemyDeath != null)
            OnEnemyDeath(enemy);
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

}
