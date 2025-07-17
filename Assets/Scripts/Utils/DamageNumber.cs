using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DamageNumber : MonoBehaviour
{
    [SerializeField] private TMP_Text damageNumberText;
    private float _floatSpeed;
    
    void Start()
    {
        _floatSpeed = Random.Range(0.1f, 1.5f);
        Destroy(gameObject, 1f);
    }

    void Update()
    {
        transform.position += Vector3.up * (Time.deltaTime * _floatSpeed);
    }

    public void SetValue(int value)
    {
        damageNumberText.text = value.ToSafeString();
    }
}
