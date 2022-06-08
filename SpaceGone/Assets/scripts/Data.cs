using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data
{
    // Start is called before the first frame update
    public float money;
    public int ship_ID;
    public Data(Info inf)
    {
        money = inf.money;
        ship_ID = inf.ship_ID;
    }
    public Data(float m, int s)
    {
        money = m;
        ship_ID = s;
    }
}
