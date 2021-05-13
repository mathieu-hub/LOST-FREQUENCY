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
    public bool isTeleport = false;
    public Transform tpDarkRoom;

    [Header("LAUNCH CINEMATIC")]
    public Animator darkPannelFade;

    [Header("ACT BOOLEAN")]
    public bool isAct01 = false;
    public bool isAct02 = false;
    public bool isAct03 = false;
    public bool isAct04 = false;

    [Header ("ACT 01")]     
    public GameObject takableModificateur;
    public GameObject takingInterface;
    public bool canTake = true;
    public bool takingObject = false;
    public bool canSpeak = true;
    public bool canTeleportToDarkRoom01 = false;
    [Space(10)]
    public GameObject wallDoor;
    public GameObject closeWall;
    public GameObject oldTVOff;
    public GameObject oldTVOn;
    public GameObject wallLightFlick;
    public GameObject wallLightOff;
    public GameObject victorianChandelierOn;
    public GameObject victorianChandelierFlick;
    public GameObject victorianChandelierOff;
    public GameObject lampOn;
    public GameObject lampOff;

    [Header("ACT 02")]
    public List<GameObject> groupLte = new List<GameObject>();
    public List<GameObject> groupLteEmettor = new List<GameObject>();
    public int indexLte = 0;
    public bool canSpawnLte = true;
    public bool canLaunchCoroutine = true;
    public bool canTeleportToBibliotheque = false;
    public Transform tpBibliotheque;

    [Header("ACT 03")]
    public GameObject emetteurHosp;
    public GameObject lightConduit;
    public bool canActivateObjects = true;
    public bool canActivateLights = false;
    public bool canDeletePlanks = false;
    public int indexBordel;
    public List<GameObject> bordels = new List<GameObject>();
    [Space(5)]
    public int indexWallLight;
    public List<Light> wallLights = new List<Light>();
    [Space(5)]
    public GameObject jammedDoor;
    public GameObject normalDoor;
    [Space(5)]
    public GameObject trepanEater;
    public bool canTriggerEvent = false;
    public GameObject triggerEvent;
    public GameObject jumpScare;
    public List<Light> wallLights02 = new List<Light>();
    public Light lightChandelier;
    public GameObject candlesGroup;
    public bool canTeleportToDarkRoom02 = false;


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
                wallDoor.SetActive(true);
                closeWall.SetActive(false);
                oldTVOn.SetActive(false);
                oldTVOff.SetActive(true);
                lampOff.SetActive(false);
                lampOn.SetActive(true);
                isAct01 = false;
                isAct02 = true;
            }
        }

        if (canTeleportToBibliotheque)
        {
            ModificateurDeFrequence.Instance.Teleportation(tpBibliotheque); // Teleportation dans Bibliothèque.

            if (isTeleport)
            {
                canTeleportToBibliotheque = false;
                isTeleport = false;
                ModificateurDeFrequence.Instance.emmetors[0].GetComponent<EmetteurType>().asAnAnomalie = true;
                ModificateurDeFrequence.Instance.emmetors.Remove(groupLteEmettor[3]);
                ModificateurDeFrequence.Instance.emmetors.Add(emetteurHosp);
                isAct02 = false;
                isAct03 = true;
            }
        }

        if (canTeleportToDarkRoom02)
        {
            ModificateurDeFrequence.Instance.Teleportation(tpDarkRoom); // 2ème Teleportation dans Dark Room.

            if (isTeleport)
            {
                canTeleportToDarkRoom02 = false;
                isTeleport = false;
                isAct03 = false;
                isAct04 = true;                
            }
        }

        if (isAct01)
        {
            FirstAct();
        }

        if (isAct02)
        {
            SecondAct();
        }

        if (isAct03)
        {
            ThirdAct();
        }

        if (isAct04)
        {
            FourthAct();
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
        yield return new WaitForSeconds(8.1f);
        SoundManager.Instance.CloseThunder.Post(gameObject);
        yield return new WaitForSeconds(0.3f);
        SoundManager.Instance.horrorLoopSalvation.Post(gameObject);
        victorianChandelierFlick.SetActive(false);
        victorianChandelierOff.SetActive(true);
        yield return new WaitForSeconds(1f);
        SoundManager.Instance.vhsTape.Post(gameObject);        
        oldTVOff.SetActive(false);
        oldTVOn.SetActive(true);
        canTeleportToDarkRoom01 = true;
    }

    void SecondAct()
    {
        if (canLaunchCoroutine && canSpawnLte)
        {
            canLaunchCoroutine = false;
            canSpawnLte = false;
            StartCoroutine(DelayLteAppear());
        }

        if (canLaunchCoroutine && groupLteEmettor[indexLte].GetComponent<EmetteurType>().asAnAnomalie == false)
        {
            canLaunchCoroutine = false;
            ModificateurDeFrequence.Instance.emmetors.Remove(groupLteEmettor[indexLte]);
            StartCoroutine(DelayLteRemove());
        }

        if (indexLte == 3)
        {
            canTeleportToBibliotheque = true;
        }
    }

    IEnumerator DelayLteAppear()
    {
        yield return new WaitForSeconds(3f);
        groupLte[indexLte].SetActive(true);
        ModificateurDeFrequence.Instance.emmetors.Add(groupLteEmettor[indexLte]);
        canLaunchCoroutine = true;
    }

    IEnumerator DelayLteRemove()
    {
        yield return new WaitForSeconds(0.3f);
        groupLte[indexLte].SetActive(false);
        yield return new WaitForSeconds(0.5f);
        indexLte++;
        yield return new WaitForSeconds(0.5f);
        canLaunchCoroutine = true;
        canSpawnLte = true;
    }

    void ThirdAct()
    {
        if (ModificateurDeFrequence.Instance.emmetors[2].GetComponent<EmetteurType>().asAnAnomalie == false)
        {
            ModificateurDeFrequence.Instance.emmetors.Remove(emetteurHosp);

            if (canActivateObjects)
            {
                for (int i = 0; i < bordels.Count ; i++)
                {
                    indexBordel = i;
                    bordels[i].SetActive(false);
                }

                if (indexBordel == bordels.Count -1)
                {
                    canActivateLights = true;
                    canActivateObjects = false;
                }
            }

            if (canActivateLights)
            {
                for (int x = 0; x < wallLights.Count ; x++)
                {
                    indexWallLight = x;
                    wallLights[x].enabled = true;
                }

                if (indexWallLight == wallLights.Count - 1)
                {
                    canDeletePlanks = true;
                    canActivateLights = false;
                }
            }

            if (!canActivateObjects && !canActivateLights)
            {
                lightConduit.SetActive(false);
                jammedDoor.SetActive(false);
                normalDoor.SetActive(true);
                trepanEater.SetActive(true);
                triggerEvent.SetActive(true);
            }            
        }

        if (canTriggerEvent)
        {
            canTriggerEvent = false;
            BlockPlayerMovement();
            StartCoroutine(SwitchOutLight());            
        }

        if (ModificateurDeFrequence.Instance.emmetors[0].GetComponent<EmetteurType>().asAnAnomalie == false)
        {
            candlesGroup.SetActive(true);
            canTeleportToDarkRoom02 = true;
        }
    }

    IEnumerator SwitchOutLight()
    {
        wallLights02[0].enabled = false;
        wallLights02[1].enabled = false;
        yield return new WaitForSeconds(1f);
        wallLights02[2].enabled = false;
        wallLights02[3].enabled = false;
        yield return new WaitForSeconds(1f);
        wallLights02[4].enabled = false;
        wallLights02[5].enabled = false;
        yield return new WaitForSeconds(1f);
        wallLights02[6].enabled = false;
        wallLights02[7].enabled = false;
        jumpScare.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        RestorePlayerMovement();
    }

    void FourthAct()
    {
        Debug.Log("JE S'APPEL GROOT");
    }
}
