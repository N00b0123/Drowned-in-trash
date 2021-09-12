using UnityEngine;

public class Destructible : MonoBehaviour, IDamage
{
    public float health = 100f;
    public GameObject destroyedVersion;
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Instantiate(destroyedVersion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
