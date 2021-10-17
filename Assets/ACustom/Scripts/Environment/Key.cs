using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] KeyType keyType;
    public enum KeyType
    {
        Blue,
        Red,
        Yellow
    }

    public KeyType GetKeyType()
    {
        return keyType;
    }
}
