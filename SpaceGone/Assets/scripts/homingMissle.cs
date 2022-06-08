using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class homingMissle : MonoBehaviour
{
    public rada Rada;
    public float speed, rotateSpeed, range;
    private Rigidbody2D rg;
    public GameObject target = null;
    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 15f);
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) target = Rada.getTarget();
        if (target == null)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
            return;
        }

        Vector2 direction = target.transform.position - transform.position;
        direction.Normalize();

        float angle = Vector3.Cross(direction, transform.up).z;

        rg.angularVelocity = -rotateSpeed * angle;
        
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (transform.name != collision.transform.name)
            Destroy(gameObject);
    }
}
