using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class joystickControlForGun : MonoBehaviour
{
    public float EngineForce = 5, maxSp;
    public bool rotateduetoSpeed = true;
    public float HP = 50, reloadTime = 0.3f;

    public Canvas canvas;
    public FixedJoystick Joystick;
    public FixedJoystick Gun;

    private float timer;
    public GameObject bullet;
    public float accuracy = 5f;
    private Rigidbody2D rg;
    private float LookRot;
    public float distance_to_gun = 0.7f;

    // Start is called before the first frame update
    void Start()
    {
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = Camera.main;
        rg = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if ((Gun.Horizontal != 0 || Gun.Vertical != 0) && timer > reloadTime)
        {
            timer = 0;
            float shootAngle = Mathf.Atan2(Gun.Horizontal, Gun.Vertical) * Mathf.Rad2Deg;
            float nacc = Random.Range(-accuracy, accuracy);
            Quaternion angle = transform.rotation;
            angle.eulerAngles = new Vector3(angle.eulerAngles.x, angle.eulerAngles.y, angle.eulerAngles.z + nacc);
            angle.eulerAngles = new Vector3(angle.eulerAngles.x, angle.eulerAngles.y, -shootAngle);

            if (bullet == null) return;

            if (bullet.GetComponent<homingMissle>() != null) bullet.tag = "player";
            Instantiate(bullet, transform.position + Vector3.up * Mathf.Cos(Mathf.Deg2Rad * shootAngle) * distance_to_gun + Vector3.right * Mathf.Sin(Mathf.Deg2Rad * shootAngle) * distance_to_gun, angle);
        }

        if (rotateduetoSpeed && rg.velocity != new Vector2(0, 0))
        {
            rg.rotation = -Mathf.Rad2Deg * Mathf.Acos(rg.velocity.y / (Mathf.Sqrt(rg.velocity.x * rg.velocity.x + rg.velocity.y * rg.velocity.y)));
            if (rg.velocity.x < 0) rg.rotation = -rg.rotation;
        }
        else if (Joystick.Direction != new Vector2(0, 0))
        {
            rg.rotation = -Mathf.Rad2Deg * Mathf.Acos(Joystick.Vertical / (Mathf.Sqrt(Joystick.Horizontal * Joystick.Horizontal + Joystick.Vertical * Joystick.Vertical)));
            if (Joystick.Horizontal < 0) rg.rotation = -rg.rotation;
        }

        if (Joystick.Horizontal == 0 && Joystick.Vertical == 0)
        {
            rg.rotation = LookRot;
            return;
        }
        rg.AddForce(new Vector2((Joystick.Horizontal * maxSp - rg.velocity.x) * EngineForce, (Joystick.Vertical * maxSp - rg.velocity.y) * EngineForce));

        LookRot = rg.rotation;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.GetComponent<damage>() == null) return;

        float damage = Random.Range(collision.transform.GetComponent<damage>().lowerdam, collision.transform.GetComponent<damage>().upperdam);
        HP -= damage;

        if (HP <= 0)
        {
            Info dmg = GameObject.Find("ship_ID").GetComponent<Info>();
            dmg.money += dmg.score;
            if (dmg.money > 1500) dmg.money = 1000;

            Debug.Log(dmg.money);
            Destroy(gameObject);

            //gameSave gs = new gameSave();
            // gs.saveGame();
        }
    }
}

