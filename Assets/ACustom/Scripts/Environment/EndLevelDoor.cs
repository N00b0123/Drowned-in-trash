using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndLevelDoor : MonoBehaviour
{
    Animator animator;
    [SerializeField] Wallet wallet;
    int residueRequire = 1000;
    int residue;
    [SerializeField] TextMeshProUGUI needResidueText;
    [SerializeField] GameObject ui;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    void OnTriggerEnter(Collider collider)
    {
        residue = wallet.GetResidue();
        if (collider.GetComponent<CharacterController>() != null)
        {
            if(residue >= residueRequire)
            {
                OpenDoor();
                AudioManager.PlaySound(AudioManager.Sound.OpenDoor, GetPosition());
            }
            else
            {
                needResidueText.SetText("Você Precisa De " + residueRequire + " Resíduos Para Abrir");
                ShowUI();
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.GetComponent<CharacterController>() != null)
        {
            HideUI();
            CloseDoor();
            Invoke(nameof(DelayCloseSound), 0.5f);
        }
    }

    void DelayCloseSound()
    {
        AudioManager.PlaySound(AudioManager.Sound.CloseDoor, GetPosition());
    }

    public void OpenDoor()
    {
        animator.SetBool("Open", true);
    }

    public void CloseDoor()
    {
        animator.SetBool("Open", false);
    }

    void ShowUI()
    {
        ui.SetActive(true);
    }

    void HideUI()
    {
        ui.SetActive(false);
    }
}
