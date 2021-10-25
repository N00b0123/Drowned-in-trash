using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MedKit : MonoBehaviour, ICollectable
{
    [SerializeField] float health;
    [SerializeField] PlayerController playerController;
    [SerializeField] TextMeshProUGUI collectUIText;
    [SerializeField] GameObject collectUI;

    public void Use()
    {
        SetText();
        playerController.SetHealth(health);
        Destroy();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    void SetText()
    {
        collectUIText.SetText("Você recuperou " + health + " de vida");
    }
}
