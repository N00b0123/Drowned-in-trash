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

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<CharacterController>() != null)
        {
            OpenDoor();
            AudioManager.PlaySound(AudioManager.Sound.OpenDoor, GetPosition());
        }

        if (collider.GetComponent<EnemyController>() != null)
        {
            OpenDoor();
            AudioManager.PlaySound(AudioManager.Sound.OpenDoor, GetPosition());
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.GetComponent<CharacterController>() != null)
        {
            CloseDoor();
            Invoke(nameof(DelayCloseSound), 0.5f);
        }

        if (collider.GetComponent<EnemyController>() != null)
        {
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
}
