using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turret : MonoBehaviour
{
    public float range, reloadTime, shootA, accuracy, HP, bonus = 3;
    private float timer;
    public GameObject[] guns, bullet;

    private void Start()
    {
        transform.tag = "enemy";
    }
    // Update is called once per frame
    void Update()
    {
        GameObject target = GameObject.Find("player");
        timer += Time.deltaTime;

        if (target == null) return;
        Vector2 front = target.transform.position - transform.position;

        float angle = Mathf.Atan2(front.y, front.x) * Mathf.Rad2Deg;

        float leftAngle = (angle + 180 + 90 + shootA) % 360, rightAngle = (angle + 180 + 90 - shootA) % 360;

        if (timer > reloadTime
            && ((leftAngle > rightAngle && transform.rotation.eulerAngles.z < leftAngle && transform.rotation.eulerAngles.z > rightAngle)
            || (leftAngle < rightAngle && (transform.rotation.eulerAngles.z < leftAngle || transform.rotation.eulerAngles.z > rightAngle)))
            && front.x * front.x + front.y * front.y < range * range)
        {
            // auto fire
            for (int j = 0; j < guns.Length; j++)
            {
                Quaternion randA = Quaternion.Euler(new Vector3(0, 0, angle - 90 + Random.Range(-accuracy, accuracy)));
                if (bullet[j].GetComponent<homingMissle>() != null)
                {
                    bullet[j].GetComponent<homingMissle>().target = target;
                    bullet[j].tag = "enemy";
                }
                Instantiate(bullet[j], guns[j].transform.position, randA);
            }
            timer = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.GetComponent<damage>() == null) return;

        float damage = Random.Range(collision.transform.GetComponent<damage>().lowerdam, collision.transform.GetComponent<damage>().upperdam);
        HP -= damage;

        if (HP <= 0)
        {
            GameObject.Find("ship_ID").GetComponent<Info>().money += bonus;
            Destroy(gameObject);
            GameObject.Find("GameController").GetComponent<GameController>().enemy_in_scene--;
        }
    }

    public bool beDestroyed()
    {
        if (HP <= 0) return true;
        return false;
    }
}
