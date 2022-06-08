using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Info : MonoBehaviour
{
    public float money = 200, score;
    public int ship_ID = 0;
    public bool SOUNDON;
    private void Start()
    {
        SOUNDON = true;
        for (int i = 0; i < Object.FindObjectsOfType<Info>().Length; i++)
        {
            if (Object.FindObjectsOfType<Info>()[i] != this)
            {
                if (Object.FindObjectsOfType<Info>()[i].name == gameObject.name) Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);

        Data dt = FileSystem.LoadFile();
        money = dt.money;
        ship_ID = dt.ship_ID;
    }
    
    /*public void LoadGame()
    {
        FileSystem.SaveFile(this);
        GameObject gob = GameObject.Find("ship_ID");
        gob.GetComponent<Info>().score = 0;
        SceneManager.LoadScene("SampleScene");
    }

    public void LoadMenu()
    {
        FileSystem.SaveFile(this);
        SceneManager.LoadScene("begin");
    }*/

    public void shipChoose(int n)
    {
        Info dmg = GameObject.Find("ship_ID").GetComponent<Info>();
        if (dmg.money < 200) return;

        dmg.money -= 200;
        dmg.ship_ID = n;
    }

    public void setTimeScale(float n)
    {
        Time.timeScale = n;
    }

    public void moreGold(float g)
    {
        money += g;
        Debug.Log(money);
        if (money > 1000) money = 1000;
    }

    public void soundOn(AudioSource au)
    {
        au.volume = 1;
    }

    public void soundOff(AudioSource au)
    {
        au.volume = 0;
    }
}
