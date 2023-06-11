using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StripAnimation : MonoBehaviour
{
    private bool check=false;
    void Update()
    {
        if(Input.anyKey)
        LoadGameScene();
    }
    public void LoadGameScene()
    {
        if(!check) {
            //TODO: UCITAJ SCENU ZA GAME OVDE
            Debug.Log("Sledeca Scena");
            check=true;
        }

    }
}
