using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillGoal : Goal
{
    //This script keeps track of kill quest's progress
    public int EnemyID { get; set; }

    public KillGoal(int enemyID, string description, bool completed, int currentAmount, int requiredAmount)
    {
        this.EnemyID = enemyID;
        this.Description = description;
        this.Completed = completed;
        this.CurrentAmount = currentAmount;
        this.RequiredAmount = requiredAmount;
    }

    public override void Init()
    {
        base.Init();
        CombatEvents.OnEnemyDeath += EnemyDied;
    }

    void EnemyDied(IEnemy enemy)
    {
        if (enemy.ID == this.EnemyID) //if the killed enemy's ID is current kill goal's enemy ID
        {
            this.CurrentAmount++;
            Evaluate(); //Goal scripts function
        }
    }
}
