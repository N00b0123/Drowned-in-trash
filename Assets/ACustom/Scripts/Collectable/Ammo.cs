using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ammo : MonoBehaviour, ICollectable
{
    [SerializeField] float ammo;
    [SerializeField] Gun gun;
    [SerializeField] TextMeshProUGUI collectUIText;
    [SerializeField] GameObject collectUI;

    public void Use()
    {
        SetText();
        gun.SetAmmo(ammo);
        Destroy();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    void SetText()
    {
        collectUIText.SetText("Você pegou " + ammo + " de Munição");
    }
}
