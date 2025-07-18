using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpButton : MonoBehaviour
{
    public TMP_Text weaponName;
    public TMP_Text weaponDescription;
    public Image weaponIcon;

    private Weapon _assignedWeapon;

    public void ActivateButton(Weapon weapon)
    {
        string description = "...";

        if (weapon.weaponLevel >= weapon.stats.Count)
            description = "Max   Level".ToUpper();
        else
            description = weapon.stats[weapon.weaponLevel].description.ToUpper();

        weaponName.text = weapon.weaponName.ToUpper();
        weaponDescription.text = description;
        weaponIcon.sprite = weapon.weaponIcon;

        _assignedWeapon = weapon;
        
    }

    public void SelectUpgrade()
    {
        _assignedWeapon.LevelUp();
        UIController.Instance.LevelUpPanelClose();
    }
}