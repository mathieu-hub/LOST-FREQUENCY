using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherEvent : MonoBehaviour
{
    public bool launchDialog01;
    public bool launchDialog02;
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

            if (launchDialog01)
            {
                SoundManager.Instance.Terence09.Post(gameObject);
                SoundManager.Instance.afterJumpscare.Stop(gameObject);
                SoundManager.Instance.ambianceDarkRoom02.Stop(gameObject);
            }

            if (launchDialog02)
            {
                SoundManager.Instance.Terence11.Post(gameObject);
            }
        }
    }

    
}
