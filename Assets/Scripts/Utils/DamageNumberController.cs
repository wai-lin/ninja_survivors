using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumberController : MonoBehaviour
{
    public static DamageNumberController Instance;
    public DamageNumber prefab;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    public void CreateNumber(float value, Vector3 position)
    {
        DamageNumber damageNumber = Instantiate(prefab, position, transform.rotation, transform);
        damageNumber.SetValue(value);
    }
}