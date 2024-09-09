using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class change : MonoBehaviour
{
    public TMP_Text text;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (GameObject.FindGameObjectsWithTag("Player").Length< 2 && timer>5){
            text.text = "GameOver: ";
        }
    }
    public void gameover()
    {
       
    }
}
