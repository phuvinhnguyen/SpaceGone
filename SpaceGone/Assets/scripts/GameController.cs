using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int enemy_in_scene = 0;
    public float spaceTime, enemyTime, distance;
    public GameObject replay;

    private GameObject VirCam, count;
    private Text score;
    private GameObject buttons, playerShip;
    public GameObject stars;
    public GameObject[] player, enemy, spaceOb;
    private float[] timer;
    private float ID;
    private int max_object = 20;

    // Start is called before the first frame update
    void Start()
    {
        count = GameObject.Find("ship_ID");
        score = GameObject.Find("score").GetComponent<Text>();

        GameObject ship_ID = GameObject.Find("ship_ID"); // dont destroy on load
        ID = ship_ID.GetComponent<Info>().ship_ID;

        playerShip = Instantiate(player[(int)ID], new Vector3(0, 0, 0), Quaternion.identity);
        playerShip.transform.name = "player";
        timer = new float[3] { 0, 0, 0 };

        VirCam = GameObject.Find("VirCam");
        VirCam.GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = playerShip.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (count.GetComponent<Info>().score > 30) max_object = 35;

        if (playerShip == null)
        {
            replay.SetActive(true);
            return;
        }

        score.text = "" + (int)count.GetComponent<Info>().score;
        
        timer[0] += Time.deltaTime;

        // spawn stars
        if (timer[0] > 0.1)
        {
            timer[0] = 0;
            float distance = 15;

            for (int i = 0; i < 3; i++)
            {
                float x = Random.Range(-distance, distance), y = Random.Range(-distance, distance);
                stars = Instantiate(stars, new Vector3(playerShip.transform.position.x + x, playerShip.transform.position.y + y, 0), Quaternion.identity);
                Destroy(stars, 5f);
            }
        }

        // spawn enemy
        timer[1] += Time.deltaTime;

        if (timer[1] > enemyTime && enemy_in_scene < max_object)
        {
            timer[1] = 0;
            int rand = Random.Range(0, enemy.Length);
            float place = Random.Range(-distance, distance);
            int updown = Random.Range(0, 2);
            if (updown == 0) updown = -1;

            Instantiate(enemy[rand], new Vector3(playerShip.transform.position.x + place, playerShip.transform.position.y + updown * Mathf.Sqrt(place * place + distance * distance), 0), Quaternion.identity);
            enemy_in_scene++;
        }

        // spawn world
        timer[2] += Time.deltaTime;

        if (timer[2] > spaceTime)
        {
            timer[2] = 0;
            int rand = Random.Range(0, spaceOb.Length);
            float place = Random.Range(-distance, distance);
            int updown = Random.Range(0, 2);
            if (updown == 0) updown = -1;

            Instantiate(spaceOb[rand], new Vector3(playerShip.transform.position.x + place, playerShip.transform.position.y + updown * Mathf.Sqrt(place * place + distance * distance), 0), Quaternion.identity);
        }
    }

}
