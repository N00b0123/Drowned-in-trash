using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLoot : MonoBehaviour
{
    [SerializeField] List<GameObject> dropObj;
    [SerializeField] int[] table = { 50, 30, 20 };
    int total;
    int randomNumber;
    GameObject dropItem;

    // Start is called before the first frame update
    void Start()
    {
        foreach(var item in table)
        {
            total = total + item;
        }
    }

    public GameObject DropItem()
    {

        randomNumber = Random.Range(0, total);

        for (int i = 0; i < table.Length; i++)
        {
            if (randomNumber <= table[i])
            {
                //instaciate
                dropItem = dropObj[i];
                return dropItem;
            }
            else
            {
                randomNumber -= table[i];
            }
        }
        return dropItem;
    }
}
