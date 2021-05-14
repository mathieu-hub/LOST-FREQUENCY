using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPassing : MonoBehaviour
{
    public bool isPassing = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isPassing = true;
        }
    }

}
