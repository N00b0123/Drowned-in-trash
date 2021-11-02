using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ammo : MonoBehaviour, ICollectable
{
    [SerializeField] int ammo;
    int ammoText;
    [SerializeField] Gun gun;
    [SerializeField] TextMeshProUGUI collectUIText;
    [SerializeField] GameObject collectUI;
    string ammoTypeText;
    public TypeOfAmmo typeOfAmmo;

    void Start()
    {
        ammoText = ammo;
    }

    public enum TypeOfAmmo
    {
        Rifle,
        Pistol,
        Shotgun,
        SMG,
    }

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
        SetTypeOfAmmoText(typeOfAmmo);
        collectUIText.SetText("Você pegou " + ammoText + " de Munição de " + ammoTypeText);
    }

    string SetTypeOfAmmoText(TypeOfAmmo typeAmmo)
    {
        switch(typeAmmo)
        {
            case TypeOfAmmo.Rifle:
                {
                    ammoTypeText = "Rifle";
                    return ammoTypeText;
                }

            case TypeOfAmmo.Pistol:
                {
                    ammoTypeText = "Pistola";
                    return ammoTypeText;
                }

            case TypeOfAmmo.Shotgun:
                {
                    ammoTypeText = "Espingarda";
                    ammoText = ammoText / 5;
                    return ammoTypeText;
                }

            case TypeOfAmmo.SMG:
                {
                    ammoTypeText = "Submetralhadora";
                    return ammoTypeText;
                }

            default:
                {
                    return null;
                }
        }
    }
}
