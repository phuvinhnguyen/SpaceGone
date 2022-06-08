using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class goldShow : MonoBehaviour
{
    Info dmg;
    Text txt;
    // Start is called before the first frame update
    void Start()
    {
        dmg = GameObject.Find("ship_ID").GetComponent<Info>();
        txt = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        txt.text = "" + dmg.money;
    }
}
