using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delay = 3f;
    public float blastRadius = 5f;
    public float explodeForce = 400f;
    public float damage = 400f;
    public GameObject explosionEffect;
    float countdown;
    bool hasExplode;

    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if(countdown <= 0f && !hasExplode)
        {
            Explode();
        }
    }

    void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        AudioManager.PlaySound(AudioManager.Sound.Grenade);
        Damage();
        MoveForce();
        Destroy(gameObject);
        hasExplode = true;
    }

    void Damage()
    {
        Collider[] collidersToDestroy = Physics.OverlapSphere(transform.position, blastRadius);

        foreach (Collider nearbyObject in collidersToDestroy)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explodeForce, transform.position, blastRadius);
            }

            IDamage dest = nearbyObject.GetComponent<IDamage>();
            if (dest != null)
            {
                dest.TakeDamage(damage);
            }
        }
    }

    void MoveForce()
    {
        Collider[] collidersToMove = Physics.OverlapSphere(transform.position, blastRadius);

        foreach (Collider nearbyObject in collidersToMove)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explodeForce, transform.position, blastRadius);
            }
        }
    }
}
