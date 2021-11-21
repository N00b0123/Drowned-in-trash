using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Residue : MonoBehaviour, ICollectable
{
    [SerializeField] int quantity;
    [SerializeField] Wallet wallet;
    [SerializeField] TextMeshProUGUI collectUIText;
    [SerializeField] GameObject collectUI;
    [SerializeField] float spawnRadius = 20;
    [SerializeField] GameObject enemyPf;
    int spawnLimit = 0;
    Transform playerPosition;
    float playerDistance;
    float elapsedTime = 0.0f;
    float secondsBetweenSpawn = 7f;


    void Start()
    {
        playerPosition = FindPlayer.instance.player.transform;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        playerDistance = Vector3.Distance(playerPosition.position, transform.position);
        if (playerDistance < spawnRadius && elapsedTime > secondsBetweenSpawn && spawnLimit < 5)
        {
            elapsedTime = 0;
            SpawnEnemy();
            spawnLimit++;
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPf, transform.position, Quaternion.identity);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }

    public void Use()
    {
        SetText();
        wallet.SetResidue(quantity);
        Destroy();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    void SetText()
    {
        collectUIText.SetText("Você pegou " + quantity + " resíduos");
    }
}
