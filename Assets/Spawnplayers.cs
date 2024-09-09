using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Spawnplayers : MonoBehaviour
{ 
    public GameObject playerprefab;
    public float timer;
    public bool k = false;
    
    
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 2)
        {
            k = true;
           
        }
        if (k)
        {
            
            PhotonNetwork.Instantiate(playerprefab.name, new Vector3(Random.Range(0,5f),0,0), Quaternion.identity);
            k = false;
        }
        
    }
}
