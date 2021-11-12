using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Wallet : MonoBehaviour
{
    int residue;
    [SerializeField] TextMeshProUGUI residueText;

    private void Update()
    {
        residueText.SetText("" + residue);
    }

    public int GetResidue()
    {
        return residue;
    }

    public void SetResidue(int quantity)
    {
        residue = residue + quantity;
    }
}
