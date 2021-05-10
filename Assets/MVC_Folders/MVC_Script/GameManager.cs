using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("GENERALS")]
    public GameObject modificateurDeFréquence;
    public Animator animTakingMf;
    public bool takingModificateur = false;
    public bool haveModificateur = false;
    [Space(5)]
    public GameObject playerEntity;
    public bool canTeleportToDarkRoom01 = false;
    public bool isTeleport = false;
    public Transform tpDarkRoom;

    [Header("LAUNCH CINEMATIC")]
    public Animator darkPannelFade;

    [Header("ACT BOOLEAN")]
    public bool isAct01 = false;
    public bool isAct02 = false;

    [Header ("ACT 01")]     
    public GameObject takableModificateur;
    public GameObject takingInterface;
    public bool canTake = true;
    public bool takingObject = false;
    public bool canSpeak = true;
    [Space(10)]
    public GameObject oldTVOff;
    public GameObject oldTVOn;
    public GameObject wallLightFlick;
    public GameObject wallLightOff;
    public GameObject victorianChandelierOn;
    public GameObject victorianChandelierFlick;
    public GameObject victorianChandelierOff;
    

    private void Awake()
    {
        Instance = this;
        animTakingMf.SetBool("TakeMf", false);
    }

    void Start()
    {
        BlockPlayerMovement();
        StartCoroutine(LaunchCinematic());
    }

    IEnumerator LaunchCinematic()
    {
        SoundManager.Instance.ambianceLightning.Post(gameObject);
        yield return new WaitForSeconds(2f);
        SoundManager.Instance.Terence01.Post(gameObject);
        yield return new WaitForSeconds(12.5f);
        darkPannelFade.SetBool("FadeOut", true);
        SoundManager.Instance.Aron01.Post(gameObject);
        isAct01 = true;
        RestorePlayerMovement();
    }

    void Update()
    {
        OpenCloseMf();

        if (canTeleportToDarkRoom01)
        {
            ModificateurDeFrequence.Instance.Teleportation(tpDarkRoom); // Teleportation dans Dark Room. 

            if (isTeleport)
            {
                canTeleportToDarkRoom01 = false;
                isTeleport = false;
                SoundManager.Instance.horrorLoopSalvation.Stop(gameObject);
                SoundManager.Instance.vhsTape.Stop(gameObject);
                SoundManager.Instance.sciFiStinger.Post(gameObject);
                isAct01 = false;
                isAct02 = true;
            }
        }

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
        if (canSpeak)
        {
            canSpeak = false;
            SoundManager.Instance.Terence02.Post(gameObject);
        }

        if (canTake)
        {
            if (takingInterface.activeSelf)
            {
                if (takingObject)
                {
                    ModificateurDeFrequence.Instance.deviceIsOn = true; 
                    SoundManager.Instance.Terence02.Stop(gameObject);
                    takingObject = false;
                    canTake = false;
                    haveModificateur = true;
                    takingModificateur = true;
                    Destroy(takableModificateur);
                    SoundManager.Instance.Aron02.Post(gameObject);
                    StartCoroutine(Dialogue01());
                }
            }
        }
    }

    IEnumerator Dialogue01()
    {
        yield return new WaitForSeconds(1f);
        SoundManager.Instance.Terence03.Post(gameObject);
        yield return new WaitForSeconds(3.7f);
        SoundManager.Instance.Aron03.Post(gameObject);
        yield return new WaitForSeconds(10.4f);
        SoundManager.Instance.Terence04.Post(gameObject);
        yield return new WaitForSeconds(5f);
        SoundManager.Instance.Aron04.Post(gameObject);
        yield return new WaitForSeconds(1.4f);
        SoundManager.Instance.Terence05.Post(gameObject);
        yield return new WaitForSeconds(6f);
        SoundManager.Instance.Terence06.Post(gameObject);
        yield return new WaitForSeconds(7.5f);
        SoundManager.Instance.Aron05.Post(gameObject);
        yield return new WaitForSeconds(7.7f);
        SoundManager.Instance.Terence07.Post(gameObject);
        yield return new WaitForSeconds(10.2f);
        SoundManager.Instance.Aron06.Post(gameObject);
        yield return new WaitForSeconds(4.5f);
        SoundManager.Instance.anomalieRadio.Post(gameObject);
        yield return new WaitForSeconds(1.7f);
        SoundManager.Instance.Aron07.Post(gameObject);
        yield return new WaitForSeconds(0.3f);
        wallLightFlick.SetActive(false);
        wallLightOff.SetActive(true);
        victorianChandelierOn.SetActive(false);
        victorianChandelierFlick.SetActive(true);
        yield return new WaitForSeconds(8.8f);
        SoundManager.Instance.CloseThunder.Post(gameObject);
        yield return new WaitForSeconds(0.3f);
        SoundManager.Instance.horrorLoopSalvation.Post(gameObject);
        SoundManager.Instance.vhsTape.Post(gameObject);
        victorianChandelierFlick.SetActive(false);
        victorianChandelierOff.SetActive(true);
        oldTVOff.SetActive(false);
        oldTVOn.SetActive(true);
        canTeleportToDarkRoom01 = true;
    }
}
