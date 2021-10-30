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
            needKey.SetText("Você Pegou A Chave " + key.GetKeyType());
            ShowUI();
            Invoke(nameof(HideUI),1f);
        } 

        KeyDoor keyDoor = collider.GetComponent<KeyDoor>();
        if (keyDoor != null)
        {
            if (ContainsKey(keyDoor.GetKeyType()))
            {
                keyDoor.OpenDoor();
                HideUI();
            }
            else
            {
                needKey.SetText("Você Precisa Da Chave " + keyDoor.GetKeyType());
                ShowUI();
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
                HideUI();
            }
            else
            {
                HideUI();
            }
        }
    }

    void ShowUI()
    {
        needKeyUI.SetActive(true);
    }

    void HideUI()
    {
        needKeyUI.SetActive(false);
    }
}
