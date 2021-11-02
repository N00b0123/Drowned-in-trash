using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GrenadeAmmo : MonoBehaviour, ICollectable
{
    [SerializeField] int ammo;
    [SerializeField] GrenadeThrower grenadeThrower;
    [SerializeField] TextMeshProUGUI collectUIText;
    [SerializeField] GameObject collectUI;

    public void Use()
    {
        SetText();
        grenadeThrower.SetAmmo(ammo);
        Destroy();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    void SetText()
    {
        collectUIText.SetText("Você pegou " + ammo + " Granadas");
    }
}
