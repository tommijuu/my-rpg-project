using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    public BaseStats[] AllClassStats;
    public bool classSelectionWindow;
    public GameObject user;

    private void OnGUI()
    {
        if (classSelectionWindow) //Creating just temporary UI for now
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

    void AssignBaseStats(int chosenClass) //apply base stats to user stats
    {
        UserStats userStats = user.GetComponent<UserStats>();

        //Class
        userStats.userClass = AllClassStats[chosenClass].userClass;

        //Base hp amounts
        userStats.currentHp = AllClassStats[chosenClass].baseHp;
        userStats.maxHp = AllClassStats[chosenClass].baseHp;

        //Base mana amounts
        userStats.currentMana = AllClassStats[chosenClass].baseMana;
        userStats.maxMana = AllClassStats[chosenClass].baseMana;

        //Base attackpower
        userStats.baseAttackPower = AllClassStats[chosenClass].baseAttackPower;
        userStats.currentAttackPower = AllClassStats[chosenClass].baseAttackPower;

        //Base attackspeed
        userStats.baseAttackSpeed = AllClassStats[chosenClass].baseAttackSpeed;
        userStats.currentAttackSpeed = AllClassStats[chosenClass].baseAttackSpeed;

        //Base dodge
        userStats.baseDodge = AllClassStats[chosenClass].baseDodge;
        userStats.currentDodge = AllClassStats[chosenClass].baseDodge;

        //Base hit percent
        userStats.baseHitPercent = AllClassStats[chosenClass].baseHitPercent;
        userStats.currentHitPercent = AllClassStats[chosenClass].baseHitPercent;

        //Base hp regen
        userStats.hpRegenTimer = AllClassStats[chosenClass].baseHpRegenTimer;
        userStats.hpRegenAmount = AllClassStats[chosenClass].baseHpRegenAmount;

        //Base mana regen
        userStats.manaRegenTimer = AllClassStats[chosenClass].baseManaRegenTimer;
        userStats.manaRegenAmount = AllClassStats[chosenClass].baseManaRegenAmount;
    }
}
