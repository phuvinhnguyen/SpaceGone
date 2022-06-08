using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loadVolume : MonoBehaviour
{
    private Slider slider;
    private damage upper;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        upper = GameObject.Find("Ship_ID").GetComponent<damage>();
    }

    // Update is called once per frame
    void Update()
    {
        upper.upperdam = slider.value;
    }
}
