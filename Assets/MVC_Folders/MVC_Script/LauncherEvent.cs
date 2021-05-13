using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherEvent : MonoBehaviour
{
    public bool isPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !isPlayed)
        {
            isPlayed = true;
            GameManager.Instance.canTriggerEvent = true;
        }
    }

    
}
