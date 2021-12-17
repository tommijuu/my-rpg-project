using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //So that the values can be edited in the editor (CharacterSelection.cs)
public class BaseStats
{
    public string userClass;
    public float baseHp, baseMana;

    public float baseAttackPower;
    public float baseAttackSpeed;

    public float baseDodge;
    public float baseHitPercent;

    public float baseHpRegenTimer, baseHpRegenAmount;
    public float baseManaRegenTimer, baseManaRegenAmount;

    public float baseXp, maxXp;
}
