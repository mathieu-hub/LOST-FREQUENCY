using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject takableModificateur;
    public GameObject takingInterface;
    public bool canTake = true;
    public bool takingModificateur = false;

    public GameObject modificateurDeFréquence;
    public bool haveModificateur = false;
    

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        FirstAct();
    }

    void FirstAct()
    {
        if (canTake)
        {
            if (takingInterface.activeSelf)
            {
                if (takingModificateur)
                {
                    canTake = false;
                    haveModificateur = true;
                }
            }
        }
    }
}
