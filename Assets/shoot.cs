using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;

public class shoot : MonoBehaviourPunCallbacks
{
    float timer =  0;
    PhotonView view;
    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer>2f && view.IsMine && Input.GetKey(KeyCode.Space)){
            GameObject go = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Cube"), transform.position, transform.rotation);
            go.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0f, 2000f));
            timer = 0;
        }
        
    }
}
