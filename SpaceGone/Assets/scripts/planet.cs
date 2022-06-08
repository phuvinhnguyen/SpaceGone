using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planet : MonoBehaviour
{
    public string fightside;
    public float rotateSpeed, timeSpawn, HP, bonus = 5;
    private float timer = 0;
    public GameObject[] turrets;
    public GameObject[] side;
    private GameObject[] already;
    private void Start()
    {
        already = new GameObject[side.Length];
        for (int i = 0; i < side.Length; i++)
            already[i] = null;
        transform.tag = fightside;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        transform.Rotate(Vector3.back * rotateSpeed * Time.deltaTime);

        if (timer > timeSpawn)
        {
            timer = 0;
            int typeTur = Random.Range(0, turrets.Length);

            for (int i = 0; i < side.Length; i++)
                if (already[i] == null)
                {
                    Vector2 dir = side[i].transform.position - transform.position;
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    Quaternion Lookr = Quaternion.Euler(new Vector3(0, 0, angle - 90));

                    already[i] = Instantiate(turrets[typeTur], side[i].transform.position, Lookr, transform);
                    break;
                }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.GetComponent<damage>() == null) return;

        float damage = Random.Range(collision.transform.GetComponent<damage>().lowerdam, collision.transform.GetComponent<damage>().upperdam);
        HP -= damage;

        if (HP <= 0)
        {
            Destroy(gameObject);
            GameObject.Find("ship_ID").GetComponent<Info>().score += bonus;
            GameObject.Find("GameController").GetComponent<GameController>().enemy_in_scene--;
        }
    }
}
