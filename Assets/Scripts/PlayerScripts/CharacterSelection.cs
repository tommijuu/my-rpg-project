using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    //private TargetingSystem targetingSystem;
    public BaseStats[] AllClassStats;
    public UserStats userStats;
    public bool classSelectionWindow;
    public GameObject userWarrior, userMage;

    //private void Start()
    //{
    //    targetingSystem = GameObject.FindGameObjectWithTag("GameController").GetComponent<TargetingSystem>();
    //}

    private void OnGUI()
    {
        if (classSelectionWindow) //Simple character selection buttons for now
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 150, 200, 40), "Warrior"))
            {
                userStats = userWarrior.GetComponent<UserStats>();
                AssignBaseStats(0);
                classSelectionWindow = false;
                Instantiate(userWarrior, transform);
                //targetingSystem.playerCombatController = userWarrior.GetComponent<PlayerCombatController>();
            }
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 40), "Mage"))
            {
                userStats = userMage.GetComponent<UserStats>();
                AssignBaseStats(1);
                classSelectionWindow = false;
                Instantiate(userMage, transform);
                //targetingSystem.playerCombatController = userMage.GetComponent<PlayerCombatController>();
            }
        }
    }

    void AssignBaseStats(int chosenClass) //apply base stats to user stats
    {
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
