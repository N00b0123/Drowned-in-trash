using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    float ammoRifle;
    float ammoPistol;
    float ammoSMG;
    float ammoShotgun;
    float ammoGrenade;

    float shield;

    void Start()
    {
        ammoRifle = 60;
        ammoPistol = 40;
        ammoSMG = 60;
        ammoShotgun = 40;
        ammoGrenade = 5;
        shield = 25;
    }

    public float GetAmmoRifle()
    {
        return ammoRifle;
    }

    public float SetAmmoRifle(float ammo)
    {
        return ammoRifle = ammoRifle + ammo;
    }

    public float GetAmmoPistol()
    {
        return ammoPistol;
    }

    public float SetAmmoPistol(float ammo)
    {
        return ammoPistol = ammoPistol + ammo;
    }

    public float GetAmmoSMG()
    {
        return ammoSMG;
    }

    public float SetAmmoSMG(float ammo)
    {
        return ammoSMG = ammoSMG + ammo;
    }

    public float GetAmmoShotgun()
    {
        return ammoShotgun;
    }

    public float SetAmmoShotgun(float ammo)
    {
        return ammoShotgun = ammoShotgun + ammo;
    }

    public float GetAmmoGrenade()
    {
        return ammoGrenade;
    }

    public float SetAmmoGrenade(float ammo)
    {
        return ammoGrenade = ammoGrenade + ammo;
    }

    public float GetShield()
    {
        return shield;
    }

    public float SetShield(float ammount)
    {
        return shield = shield + ammount;
    }
}
