using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmetteurType : MonoBehaviour
{
    public bool emettorRadio;
    public bool emmettorTV;

    public bool asAnAnomalie;
    public bool canTeleport;

    public AK.Wwise.Event soundEvent;
    public bool launchSound = true;

    void Start()
    {
        
    }

    void Update()
    {
        if (emettorRadio)
        {
            if (asAnAnomalie)
            {
                if (launchSound)
                {
                    launchSound = false;
                    soundEvent.Post(gameObject);
                }
            }
            else if (!asAnAnomalie)
            {
                soundEvent.Stop(gameObject);
            }
        }
    }
}
