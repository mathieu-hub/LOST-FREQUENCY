using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject takableModificateur;
    public GameObject takingInterface;
    public bool canTake = true;
    public bool takingObject = false;

    public GameObject modificateurDeFréquence;
    public Animator animTakingMf;
    public bool takingModificateur = false;
    public bool haveModificateur = false;
    

    private void Awake()
    {
        Instance = this;
        animTakingMf.SetBool("TakeMf", false);
    }

    void Start()
    {
        
    }

    void Update()
    {
        OpenCloseMf();
        FirstAct();
    }

    void OpenCloseMf()
    {
        if (haveModificateur)
        {
            if (takingModificateur)
            {
                animTakingMf.SetBool("TakeMf", true);
            }
            else if (!takingModificateur)
            {
                animTakingMf.SetBool("TakeMf", false);
            }
        }
    }

    void FirstAct()
    {
        if (canTake)
        {
            if (takingInterface.activeSelf)
            {
                if (takingObject)
                {
                    takingObject = false;
                    canTake = false;
                    haveModificateur = true;
                    takingModificateur = true;
                    Destroy(takableModificateur);
                }
            }
        }
    }
}
