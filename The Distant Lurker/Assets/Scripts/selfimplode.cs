using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfimplode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void Close()
    {
        Destroy(gameObject);
    }
}
