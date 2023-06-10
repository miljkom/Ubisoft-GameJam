using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float battery;
    public float corrosion;
    public float water;

    public Player()
    {
        this.battery = 100f;
        this.corrosion = 100f;
        this.water = 100f;
    }
}
