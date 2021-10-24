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
        AudioManager audio = FindObjectOfType<AudioManager>();
        if (collider.GetComponent<CharacterController>() != null)
        {
            OpenDoor();
            audio.Play("OpenDoor");
        }

        if (collider.GetComponent<EnemyController>() != null)
        {
            OpenDoor();
            audio.Play("OpenDoor");
        }
    }

    void OnTriggerExit(Collider collider)
    {
        AudioManager audio = FindObjectOfType<AudioManager>();
        if (collider.GetComponent<CharacterController>() != null)
        {
            CloseDoor();
            audio.Play("CloseDoor");
        }

        if (collider.GetComponent<EnemyController>() != null)
        {
            CloseDoor();
            audio.Play("CloseDoor");
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
