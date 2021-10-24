using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : MonoBehaviour, ICollectable
{
    [SerializeField] float health;
    [SerializeField] PlayerController playerController;

    public void Use()
    {
        playerController.SetHealth(health);
        Destroy();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
