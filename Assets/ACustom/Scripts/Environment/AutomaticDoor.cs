using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDoor : MonoBehaviour
{
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<CharacterController>() != null)
        {
            OpenDoor();
            AudioManager.PlaySound(AudioManager.Sound.OpenDoor);
        }

        if (collider.GetComponent<EnemyController>() != null)
        {
            OpenDoor();
            AudioManager.PlaySound(AudioManager.Sound.OpenDoor);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.GetComponent<CharacterController>() != null)
        {
            CloseDoor();
            AudioManager.PlaySound(AudioManager.Sound.CloseDoor);
        }

        if (collider.GetComponent<EnemyController>() != null)
        {
            CloseDoor();
            AudioManager.PlaySound(AudioManager.Sound.CloseDoor);
        }
    }

    public void OpenDoor()
    {
        animator.SetBool("Open", true);
    }

    public void CloseDoor()
    {
        animator.SetBool("Open", false);
    }
}
