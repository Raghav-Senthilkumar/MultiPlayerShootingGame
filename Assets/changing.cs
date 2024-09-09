using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changing : MonoBehaviour
{
    // Start is called before the first frame update
    public void show(GameObject obj)
    {
        obj.SetActive(true);

    }
    public void hide(GameObject obj) {
        obj.SetActive(false);
    }

}
