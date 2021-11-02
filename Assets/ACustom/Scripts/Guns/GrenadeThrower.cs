using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GrenadeThrower : MonoBehaviour
{
    [SerializeField] float throwForce = 20f;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject grenadePrefab;
    [SerializeField] int grenadeAmmo = 3;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && grenadeAmmo > 0)
        {
            ThrowGrenade();  
        }
        text.SetText(grenadeAmmo + "");
    }
    void ThrowGrenade()
    {
        GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
        grenadeAmmo--;
    }

    public void SetAmmo(int ammo)
    {
        grenadeAmmo = grenadeAmmo + ammo;
    }
}
