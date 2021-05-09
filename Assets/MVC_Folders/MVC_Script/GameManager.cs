using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject modificateurDeFr�quence;
    public Animator animTakingMf;
    public bool takingModificateur = false;
    public bool haveModificateur = false;
    
    [Header ("ACT 01")]     
    public GameObject takableModificateur;
    public GameObject takingInterface;
    public bool canTake = true;
    public bool takingObject = false;
          
    [Header("ACT BOOLEAN")]
    public bool isAct01;
    

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

        if (isAct01)
        {
            FirstAct();
        }
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

    void BlockPlayerMovement()
    {
        PlayerController.Instance.walkSpeed = 0;
        PlayerController.Instance.runSpeed = 0;
        PlayerController.Instance.crouchSpeed = 0;
        PlayerController.Instance.proneSpeed = 0;
    }

    void RestorePlayerMovement()
    {
        PlayerController.Instance.walkSpeed = 3;
        PlayerController.Instance.runSpeed = 7;
        PlayerController.Instance.crouchSpeed = 2;
        PlayerController.Instance.proneSpeed = 1;
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
