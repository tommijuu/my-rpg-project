using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal
{
    //This script is a base class to keep count of the quests goals, there will be a KillGoal and GatheringGoal for example

    public string Description { get; set; }
    public bool Completed { get; set; }
    public int CurrentAmount { get; set; }
    public int RequiredAmount { get; set; }

    public virtual void Init()
    {
        //killgoal, gathergoal etc use this in the future to initialize them
    }

    public void Evaluate()
    {
        if (CurrentAmount >= RequiredAmount)
        {
            Complete();
        }
    }

    public void Complete()
    {
        Completed = true;
    }
}

