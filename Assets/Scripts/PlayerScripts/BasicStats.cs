using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //So that the values can be edited in the editor
public class BasicStats
{
    public string userClass;
    public float baseAttackPower;
    public float currentAttackPower;
    public float baseAttackSpeed;
    public float currentAttackSpeed;
    public float baseDodge;
    public float currentDodge;
    public float baseHitPercent;
    public float currentHitPercent;
}
