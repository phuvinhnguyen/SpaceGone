using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rada : MonoBehaviour
{
    private GameObject target = null;
    public GameObject getTarget()
    {
        return target;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (target == null
            && collision.gameObject.tag == "enemy")
        {
            target = collision.gameObject;
        }
    }
}
