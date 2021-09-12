using UnityEngine;

public class Destructible : MonoBehaviour, IDamage
{
    public float health = 100f;
    public GameObject destroyedVersion;
    public bool isWoodBox;
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
        PlaySound();
        Destroy(gameObject);
    }

    void PlaySound()
    {
        if (isWoodBox)
        {
            FindObjectOfType<AudioManager>().Play("Wood Box Breaking");
        }
    }
}
