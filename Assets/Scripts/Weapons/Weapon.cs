using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string weaponName;
    public Sprite weaponIcon;
    public int weaponLevel = 1;
    public List<WeaponStats> stats;

    public void LevelUp()
    {
        if (weaponLevel >= stats.Count) return;
        weaponLevel++;
    }
}

[System.Serializable]
public class WeaponStats
{
    public float cooldown;
    public float duration;
    public float range;
    public float damage;
    public float damageSpeed;
    public string description;
}