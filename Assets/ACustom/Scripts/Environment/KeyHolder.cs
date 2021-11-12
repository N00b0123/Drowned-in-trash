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
            if(key.GetKeyType() == Key.KeyType.Azul)
            {
                needKey.SetText("<color=#11166F>Você Pegou A Chave " + key.GetKeyType() + " </color>");
            }
            if (key.GetKeyType() == Key.KeyType.Vermelha)
            {
                needKey.SetText("<color=#7a1702>Você Pegou A Chave " + key.GetKeyType() + " </color>");
            }
            if (key.GetKeyType() == Key.KeyType.Amarela)
            {
                needKey.SetText("<color=#FFFF00>Você Pegou A Chave " + key.GetKeyType() + " </color>");
            }
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
                if (keyDoor.GetKeyType() == Key.KeyType.Azul)
                {
                    needKey.SetText("<color=#11166F>Você Precisa Da Chave " + keyDoor.GetKeyType() + " </color>");
                }
                if (keyDoor.GetKeyType() == Key.KeyType.Vermelha)
                {
                    needKey.SetText("<color=#7a1702>Você Precisa Da Chave " + keyDoor.GetKeyType() + " </color>");
                }
                if (keyDoor.GetKeyType() == Key.KeyType.Amarela)
                {
                    needKey.SetText("<color=#FFFF00>Você Precisa Da Chave " + keyDoor.GetKeyType() + " </color>");
                }
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
