using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    [SerializeField] Key.KeyType keyType;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public Key.KeyType GetKeyType()
    {
        return keyType;
    }

    public void OpenDoor()
    {
        animator.SetBool("Open", true);
        AudioManager.PlaySound(AudioManager.Sound.OpenDoor, GetPosition());
    }

    public void CloseDoor()
    {
        animator.SetBool("Open", false);
        AudioManager.PlaySound(AudioManager.Sound.CloseDoor, GetPosition());
    }
}
