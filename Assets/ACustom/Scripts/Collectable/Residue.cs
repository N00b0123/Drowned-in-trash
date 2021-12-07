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
    GameObject enemyPf;
    int spawnLimit = 0;
    Transform playerPosition;
    float playerDistance;
    float elapsedTime = 0.0f;
    float secondsBetweenSpawn = 7f;
    int randomNumber, total;

    [SerializeField] List<GameObject> dropPf;
    int[] table = { 30, 40, 30, 40 };


    void Start()
    {
        playerPosition = FindPlayer.instance.player.transform;

        foreach (var item in table)
        {
            total = total + item;
        }
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        //TODO verificar desempenho
        playerDistance = Vector3.Distance(playerPosition.position, transform.position);
        if (playerDistance < spawnRadius && elapsedTime > secondsBetweenSpawn && spawnLimit < 5)
        {
            elapsedTime = 0;
            DropItem();
            spawnLimit++;
        }
    }

    public void DropItem()
    {
        randomNumber = Random.Range(0, total);

        for (int i = 0; i < table.Length; i++)
        {
            if (randomNumber <= table[i])
            {
                enemyPf = dropPf[i];
                SpawnEnemy();
                return;
            }
            else
            {
                randomNumber -= table[i];
            }
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
