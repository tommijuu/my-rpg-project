using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlayThis : Quest
{
    void Start()
    {
        QuestName = "Humans are sheep";
        Description = "Kill the sheep";
        ExperienceReward = 100;

        //This is a kill quest, so add kill goals
        Goals.Add(new KillGoal(0, "Kill 5 humans.", false, 0, 5));
        Goals.Add(new KillGoal(1, "Kill 2 sheep", false, 0, 2));

        Goals.ForEach(g => g.Init());
    }
}
