using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractCollectable : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        ICollectable collectable = collider.GetComponent<ICollectable>();
        if (collectable != null)
        {
            collectable.Use();
        }
    }
}
