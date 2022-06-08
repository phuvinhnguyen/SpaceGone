using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour
{
    public float EngineForce = 5, maxSp;
    public bool rotateduetoSpeed = true;
    public float HP = 50;

    public Canvas canvas;
    public FixedJoystick Joystick;

    private float timer;
    private GameObject bullet;
    private float accuracy = 5f;
    private Rigidbody2D rg;
    private float LookRot;
    private bool straigth = true;
    private float distance_to_gun = 0.7f;

    public void setBullet(GameObject nbullet)
    {
        bullet = nbullet;
    }

    public void setDistance(float distance)
    {
        distance_to_gun = distance;
    }

    public void setAccuracy(float naccuracy = 5)
    {
        accuracy = naccuracy;
    }

    public void shootPre(bool nstraight)
    {
        straigth = nstraight;
    }

    public void OnFire(float shootAngle)
    {
        float nacc = Random.Range(-accuracy, accuracy);
        Quaternion angle = transform.rotation;
        angle.eulerAngles = new Vector3(angle.eulerAngles.x, angle.eulerAngles.y, angle.eulerAngles.z + nacc);
        if (!straigth)
        {
            angle.eulerAngles = new Vector3(angle.eulerAngles.x, angle.eulerAngles.y, angle.eulerAngles.z - shootAngle);
        }

        if (bullet == null) return;

        if (bullet.GetComponent<homingMissle>() != null) bullet.tag = "player";
        Instantiate(bullet, transform.position + transform.up * Mathf.Cos(Mathf.Deg2Rad * shootAngle) * distance_to_gun + transform.right * Mathf.Sin(Mathf.Deg2Rad * shootAngle) * distance_to_gun, angle);
    }

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
        if (rotateduetoSpeed && rg.velocity != new Vector2(0,0))
        {
            float angle = -Mathf.Rad2Deg * Mathf.Acos(rg.velocity.y / (Mathf.Sqrt(rg.velocity.x * rg.velocity.x + rg.velocity.y * rg.velocity.y)));
            
            if (rg.velocity.x < 0) angle = -angle;
            Quaternion q = Quaternion.Euler(0, 0, angle);
            
            transform.rotation = q;
            //Quaternion.Slerp(transform.rotation, q, 0.2f);
        }
        else if (!rotateduetoSpeed && Joystick.Direction != new Vector2(0,0))
        {
            float angle = -Mathf.Rad2Deg * Mathf.Acos(Joystick.Vertical / (Mathf.Sqrt(Joystick.Horizontal * Joystick.Horizontal + Joystick.Vertical * Joystick.Vertical)));

            if (Joystick.Horizontal < 0) angle = -angle; 
            Quaternion q = Quaternion.Euler(0, 0, angle);
            transform.rotation = q;
            //Quaternion.Slerp(transform.rotation, q, 0.2f); 
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

            Destroy(gameObject);

            //gameSave gs = new gameSave();
           // gs.saveGame();
        }
    }
}
