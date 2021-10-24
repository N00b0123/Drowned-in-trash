using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyHolder : MonoBehaviour
{
    List<Key.KeyType> keyList;
    [SerializeField] TextMeshProUGUI needKey;
    [SerializeField] GameObject needKeyUI;

    void Awake()
    {
        keyList = new List<Key.KeyType>();
    }

    void AddKey(Key.KeyType keyType)
    {
        keyList.Add(keyType);
    }

    void RemoveKey(Key.KeyType keyType)
    {
        keyList.Remove(keyType);
    }

    public bool ContainsKey(Key.KeyType keyType)
    {
        return keyList.Contains(keyType);
    }

    private void OnTriggerEnter(Collider collider)
    {
        Key key = collider.GetComponent<Key>();
        if (key != null)
        {
            AddKey(key.GetKeyType());
            Destroy(key.gameObject);
        } 

        KeyDoor keyDoor = collider.GetComponent<KeyDoor>();
        if (keyDoor != null)
        {
            if (ContainsKey(keyDoor.GetKeyType()))
            {
                keyDoor.OpenDoor();
                needKeyUI.SetActive(false);
            }
            else
            {
                needKey.SetText("voce precisa da chave " + keyDoor.GetKeyType());
                needKeyUI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        KeyDoor keyDoor = collider.GetComponent<KeyDoor>();
        if (keyDoor != null)
        {
            if (ContainsKey(keyDoor.GetKeyType()))
            {
                keyDoor.CloseDoor();
                needKeyUI.SetActive(false);
            }
            else
                needKeyUI.SetActive(false);
        }
    }
}
