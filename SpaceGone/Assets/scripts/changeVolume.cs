using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeVolume : MonoBehaviour
{
    AudioSource audio;
    Info gob;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        gob = GameObject.Find("ship_ID").GetComponent<Info>();
    }

    // Update is called once per frame
}
