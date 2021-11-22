using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour, IDamage
{
    public float health = 100f;
    [SerializeField] GameObject destroyedVersion;
    [SerializeField] List<GameObject> dropObj;
    [SerializeField] int[] table = { 50, 30, 30, 20, 10, 5, 5, 5, 5, 5 };
    [SerializeField] bool isWoodBox;
    GameObject drop;
    int total;
    int randomNumber;
    Vector3 dropPosition, brokenPosition;

    void Start()
    {
        foreach (var item in table)
        {
            total = total + item;
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void TakeDamage(float amount)
    {
        if (gameObject != null)
        {
            health -= amount;
            if (health <= 0)
            {
                Die();
            }
        }
    }

    public void DropItem()
    {
        randomNumber = Random.Range(0, total);

        for (int i = 0; i < table.Length; i++)
        {
            if (randomNumber <= table[i])
            {
                drop = dropObj[i];
                return;
            }
            else
            {
                randomNumber -= table[i];
            }
        }
    }

    void Die()
    {
        if (gameObject != null)
        {
            DropItem();
            InstanceBroken();
            PlaySound();
            DropInstance();
            Destroy(gameObject);
        }
        else return;
    }

    void InstanceBroken()
    {
        brokenPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        if (gameObject != null)
        {
            Instantiate(destroyedVersion, brokenPosition, transform.rotation);
        }
        else return;
    }

    void DropInstance()
    {
        dropPosition = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
        if (gameObject != null)
        {
            Instantiate(drop, dropPosition, Quaternion.identity);
        }
        else return;
    }

    void PlaySound()
    {
        if (gameObject != null)
        {
            if (isWoodBox)
            {
                AudioManager.PlaySound(AudioManager.Sound.WoodBoxBreaking, GetPosition());
            }
        }
        else return;
    }
}
