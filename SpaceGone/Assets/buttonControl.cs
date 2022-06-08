using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class buttonControl : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] buy, play;
    private bool SOUNDON;
    Info dmg;
    private void Start()
    {
        dmg = GameObject.Find("ship_ID").GetComponent<Info>();
        SOUNDON = dmg.SOUNDON;
        if (SceneManager.GetActiveScene().name == "begin")
        {
            if (dmg.ship_ID >= buy.Length || dmg.ship_ID >= play.Length) return;
            GameObject buyBT = buy[dmg.ship_ID], playBT = play[dmg.ship_ID];
            buyBT.SetActive(false);
            playBT.SetActive(true);
        }
        if (!SOUNDON)
        {
            Button[] bt = GameObject.FindObjectsOfType<Button>();
            for (int i = 0; i < bt.Length; i++)
            {
                if (bt[i].name == "volumeOn")
                {
                    bt[i].onClick.Invoke();
                    break;
                }
            }
        }
    }
    public void shipChoose(int n)
    {
        Info dmg = GameObject.Find("ship_ID").GetComponent<Info>();
        if (dmg.money < 200) return;

        dmg.money -= 200;
        GameObject buyBT = buy[dmg.ship_ID], playBT = play[dmg.ship_ID];
        buyBT.SetActive(true);
        playBT.SetActive(false);
        dmg.ship_ID = n;
        buy[n].SetActive(false);
        play[n].SetActive(true);
        dmg.shipChoose(n);
    }

    public void LoadGame()
    {
        GameObject gob = GameObject.Find("ship_ID");
        FileSystem.SaveFile(gob.GetComponent<Info>());
        gob.GetComponent<Info>().score = 0;
        SceneManager.LoadScene("SampleScene");
    }

    public void LoadMenu()
    {
        GameObject gob = GameObject.Find("ship_ID");
        FileSystem.SaveFile(gob.GetComponent<Info>());
        SceneManager.LoadScene("begin");
    }

    public void setTimeScale(float n)
    {
        Time.timeScale = n;
    }

    public void moreGold(float g)
    {
        admob ab = new admob();

        if (ab.ads())
        {
            Info tm = GameObject.FindObjectOfType<Info>();
            tm.money += g;
            if (tm.money > 1000) tm.money = 1000;
        }
    }

    public void soundOn(AudioSource au)
    {
        SOUNDON = true;
        dmg.SOUNDON = true;
            au.volume = 1;
    }

    public void _soundOn_()
    {
        SOUNDON = true;
        dmg.SOUNDON = true;
    }

    public void _soundOff_()
    {
        SOUNDON = false;
        dmg.SOUNDON = false;
    }


    public void soundOff(AudioSource au)
    {
        SOUNDON = false;
        dmg.SOUNDON = false;
            au.volume = 0;
    }
}
