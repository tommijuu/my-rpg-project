using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserStats : MonoBehaviour
{
    //Base class for player's stats, yet to be implemented

    public string userName;
    public int level;
    public string userClass;

    public float currentHp, maxHp, currentMana, maxMana;

    public float baseAttackPower, currentAttackPower;
    public float baseAttackSpeed, currentAttackSpeed;

    public float baseDodge, currentDodge;
    public float baseHitPercent, currentHitPercent;

    public float hpRegenTimer, hpRegenAmount;

    public float manaRegenTimer, manaRegenAmount;

    public float currentXp, maxXp;

    public bool isDead;
}
