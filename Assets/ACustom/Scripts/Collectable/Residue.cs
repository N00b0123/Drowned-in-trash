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
