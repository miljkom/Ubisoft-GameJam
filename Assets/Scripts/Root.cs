using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    public int health;
    public int timer;

    public Root(int health, int timer)
    {
        this.health = health;
        this.timer = timer;
    }
}
