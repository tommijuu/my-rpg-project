using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    public BasicStats[] AllClassStats;
    public bool classSelectionWindow;
    public GameObject user;

    private void OnGUI()
    {
        if (classSelectionWindow)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 150, 200, 40), "Warrior"))
            {
                AssignBaseStats(0);
                classSelectionWindow = false;
            }
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 40), "Mage"))
            {
                AssignBaseStats(1);
                classSelectionWindow = false;
            }
        }
    }

    void AssignBaseStats(int chosenClass) //apply basic stats to user stats
    {
        UserStats userStats = user.GetComponent<UserStats>();

        userStats.userClass = AllClassStats[chosenClass].userClass;

        userStats.baseAttackPower = AllClassStats[chosenClass].baseAttackPower;
        userStats.currentAttackPower = AllClassStats[chosenClass].currentAttackPower;

        userStats.baseAttackSpeed = AllClassStats[chosenClass].baseAttackSpeed;
        userStats.currentAttackSpeed = AllClassStats[chosenClass].currentAttackSpeed;

        userStats.baseDodge = AllClassStats[chosenClass].baseDodge;
        userStats.currentDodge = AllClassStats[chosenClass].currentDodge;

        userStats.baseHitPercent = AllClassStats[chosenClass].baseHitPercent;
        userStats.currentHitPercent = AllClassStats[chosenClass].currentHitPercent;

    }
}
