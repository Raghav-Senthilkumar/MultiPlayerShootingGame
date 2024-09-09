using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEditor;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    PhotonView PV;
    bool j = false;
    bool once = true;
    [SerializeField] TMP_Text text;
    public GameObject k;
    //public GameObject canvas;
    //[SerializeField] TMP_Text roomNameText;


    public float lookSpeed = 2f;
    public float lookXLimit = 45f;


    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;
    int score;
    public bool canMove = true;
    


   // CharacterController characterController;


    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        score = 10;
            //roomNameText.text = PhotonNetwork.NickName;
        if (PV.IsMine)
        {
            j = true;
            if (PhotonNetwork.IsMasterClient)
            {
                transform.position = new Vector3(10f, 5f, 0);
                once = false;
            }
            else
            {
                transform.position = new Vector3(0, 5f, 0);
                once = false;
            }
            
            
        }
        else
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
        }
        Debug.Log("Start: "+ GameObject.FindGameObjectsWithTag("Player").Length);


    }
    private void Update()
    {
        if (!PV.IsMine)
            return;

        if (PV.IsMine && j)
        {
            
            Vector3 forward = transform.TransformDirection(Vector3.forward)*10f;
            Vector3 right = transform.TransformDirection(Vector3.right);
            float curSpeedX = Input.GetAxis("Vertical");
            float curSpeedY =Input.GetAxis("Horizontal");
            moveDirection = (forward * curSpeedX*Time.deltaTime) + (right * curSpeedY*Time.deltaTime);
            transform.position += moveDirection;
            transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
            

            

        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        
            if (collision.transform.gameObject.name.Equals("Cube(Clone)")&& PV.IsMine)
            {
            //PhotonNetwork.Destroy(this.gameObject);

            PhotonNetwork.Destroy(this.gameObject);
            PhotonNetwork.Destroy(collision.transform.gameObject);
            //text.text = "GameOver: " + PhotonNetwork.NickName;
//            gameObject.GetComponent<change>().gameover();
            Debug.Log("After: " + GameObject.FindGameObjectsWithTag("Player").Length);
        }

        
    }

}
