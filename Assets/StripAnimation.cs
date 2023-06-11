using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            SceneManager.LoadScene("SampleScene");
            check=true;
        }
    }
    
    
}
