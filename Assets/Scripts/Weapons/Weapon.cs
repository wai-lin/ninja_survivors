using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int weaponLevel = 1;
    public List<WeaponStats> stats;
}

[System.Serializable]
public class WeaponStats
{
    public float cooldown;
    public float duration;
    public float range;
    public float damage;
    public float damageSpeed;
}