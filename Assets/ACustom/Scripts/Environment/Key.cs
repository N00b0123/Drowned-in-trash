using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] KeyType keyType;
    public enum KeyType
    {
        Azul,
        Vermelha,
        Amarela
    }

    public KeyType GetKeyType()
    {
        return keyType;
    }
}
