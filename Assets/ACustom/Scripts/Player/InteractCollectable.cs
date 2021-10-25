using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractCollectable : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject textUI;

    private void OnTriggerEnter(Collider collider)
    {
        ICollectable collectable = collider.GetComponent<ICollectable>();
        if (collectable != null)
        {
            collectable.Use();
            ShowUI();
            Invoke(nameof(HideUI), 1f);
        }
    }

    void ShowUI()
    {
        textUI.SetActive(true);
    }

    void HideUI()
    {
        textUI.SetActive(false);
    }
}
