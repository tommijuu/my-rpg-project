using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Quest : MonoBehaviour
{
    //This script is a base class for quests

    public List<Goal> Goals { get; set; } = new List<Goal>();
    public string QuestName { get; set; }
    public string Description { get; set; }
    public int ExperienceReward { get; set; }
    public Item ItemReward { get; set; }
    public bool Completed { get; set; }

    public void CheckGoals()
    {
        Completed = Goals.All(g => g.Completed); //if all goals of the quest are completed, set completed true
        if (Completed) GiveReward();
    }

    void GiveReward()
    {
        //Implement rewards here
        Debug.Log("Congratulations, U GOT NOTHING");
    }
}
