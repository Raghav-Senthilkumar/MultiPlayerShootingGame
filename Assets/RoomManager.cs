using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;


public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;
    PhotonView PV;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.buildIndex == 1) // We're in the game scene
        {
            
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "TankFree_Green"), new Vector3(10f, 0, 0), Quaternion.identity);
            
            
            
        }
    }
}