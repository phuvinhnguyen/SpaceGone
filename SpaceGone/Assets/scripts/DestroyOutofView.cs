using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutofView : MonoBehaviour
{
    private float Distance = 35;
    private Vector2 dis;
    private GameObject enemy;
    
    void Update()
    {
        enemy = GameObject.Find("player");

        if (enemy != null)
            dis = enemy.transform.position;

        Vector2 distance = (Vector2)transform.position - dis;

        if (distance.magnitude > Distance)
            Destroy(gameObject);
    }
}
