using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherEvent : MonoBehaviour
{
    public bool isPlayed = false;
    public bool isTriggerEvent;
    public bool launchAct06;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !isPlayed)
        {
            isPlayed = true;

            if (isTriggerEvent)
            {
                GameManager.Instance.canTriggerEvent = true;
            }

            if (launchAct06)
            {
                GameManager.Instance.isAct05 = false;
                GameManager.Instance.isAct06 = true;
            }
        }
    }

    
}
