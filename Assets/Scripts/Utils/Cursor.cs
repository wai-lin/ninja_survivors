using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    [SerializeField] private Texture2D cursor;

    void Start()
    {
        UnityEngine.Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }
}