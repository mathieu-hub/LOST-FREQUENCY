using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ThunderWire.CrossPlatform.Input;

public class ModificateurDeFrequence : MonoBehaviour
{
    public static ModificateurDeFrequence Instance;
    public ChangeFrequence changeFrequence;
    public RebindBar rebindBar;

    [Header("ON / OFF")]
    public bool deviceIsOn = false;
    public GameObject interruptorOnOff;
    public Animator animatorOnOff;
    public GameObject ledOnOff;

    [Header("VOYAGE TRANSFREQUENTIEL ACTIVATION")]
    public bool activeVoyageTf = false;
    public GameObject interruptorVoyageTf;
    public Animator animatorVoyageTf;
    public GameObject ledVoyageTf;

    [Header("VOYAGE TRANSFREQUENTIEL DETECTION")]
    public bool voyageTfAvailable = false; // Variable à changer 
    public GameObject availableLed;
    public GameObject disavailableLed;

    [Header("ANOMALIE DETECTEUR")]
    public int anomalieSignal; // Variable à changer
    public List<GameObject> anomalieLeds = new List<GameObject>();

    [Header("RÉPARER FRÉQUENCES")]
    public bool rebindingFrequence = false;
    public GameObject rebindBarObject;
    public GameObject textIsRepair;
    public GameObject textToRepair;

    [Space (10)]
    public GameObject recepteur;
    public GameObject emetteur;
    public List<GameObject> emmetors = new List<GameObject>();

    public float distance;
    public float captedDistance;
    
    [Space(10)]
    public float distanceZone01;
    public float distanceZone02;
    public float distanceZone03;
    public float distanceZone04;
    public float distanceZone05;
       
    [Header("ECRAN")]
    public GameObject indicateurFrequence;    
    public int detectedFrequence;
    public string detectedAnomalie;
    public GameObject screenDisplayGroup;
    public GameObject canvasRebindBar;

    [Header("MATERIALS REFERENCES")]
    public List<Material> materials = new List<Material>();   
    

    private void Awake()
    {
        Instance = this;        
    }

    private void Start()
    {
        ledOnOff.GetComponent<ChangeMaterial>().actualMaterial = materials[0];
        ledVoyageTf.GetComponent<ChangeMaterial>().actualMaterial = materials[0];
        availableLed.GetComponent<ChangeMaterial>().actualMaterial = materials[0];
        disavailableLed.GetComponent<ChangeMaterial>().actualMaterial = materials[0];

        detectedFrequence = 0;

        rebindBar.max = 100;
        rebindBar.valeur = 0;
    }

    private void Update()
    {        
        InterruptorOnOff();
        InterruptorVoyageTf();
        DetectEmetteur();
        AnomalieLeds();
        RebindFrequence();
        VoyageTf();
    }

    

    void InterruptorOnOff()
    {
        // Activation et Désactivation des interrupteurs
        if (deviceIsOn) // Allumage du boîtier
        {
            animatorOnOff.SetBool("switchOn", true);
            ledOnOff.GetComponent<ChangeMaterial>().actualMaterial = materials[1];
            screenDisplayGroup.SetActive(true);
            canvasRebindBar.SetActive(true);
        }
        else if (!deviceIsOn) // Extinction du boîtier
        {
            animatorOnOff.SetBool("switchOn", false);
            ledOnOff.GetComponent<ChangeMaterial>().actualMaterial = materials[0];
            screenDisplayGroup.SetActive(false);
            canvasRebindBar.SetActive(false);

            if (activeVoyageTf)
            {
                activeVoyageTf = false;
            }
        }
    }

    void InterruptorVoyageTf()
    {
        if (activeVoyageTf)
        {
            animatorVoyageTf.SetBool("tfSwitchOn", true);
            ledVoyageTf.GetComponent<ChangeMaterial>().actualMaterial = materials[1];
        }
        else if (!activeVoyageTf)
        {
            animatorVoyageTf.SetBool("tfSwitchOn", false);
            ledVoyageTf.GetComponent<ChangeMaterial>().actualMaterial = materials[0];
        }
    }

    void DetectEmetteur()
    {
        detectedAnomalie = "Anomalie : Niveau " + anomalieSignal;

        //Capter l'emetteur le plus proche du recepteur
        captedDistance = Mathf.Infinity;        
        
        foreach(GameObject emmetor in emmetors)
        {
            distance = Vector3.Distance(emmetor.transform.position, recepteur.transform.position);

            if(distance < captedDistance)
            {
                captedDistance = distance;
                emetteur = emmetor;
            }
        }

        //Detection Feedbacks On Modificateur
        if (captedDistance >= distanceZone01)
        {
            detectedFrequence = 93;
            anomalieSignal = 0;
        }
        else if (captedDistance <= distanceZone01 && captedDistance >= distanceZone02)
        {
            detectedFrequence = 170;
            
            if (emetteur.GetComponent<EmetteurType>().asAnAnomalie == true)
            {
                anomalieSignal = 1;
            }
            else
            {
                anomalieSignal = 0;
            }
        }
        else if (captedDistance <= distanceZone02 && captedDistance >= distanceZone03)
        {
            detectedFrequence = 197;

            if (emetteur.GetComponent<EmetteurType>().asAnAnomalie == true)
            {
                anomalieSignal = 2;
            }
            else
            {
                anomalieSignal = 0;
            }
        }
        else if (captedDistance <= distanceZone03 && captedDistance >= distanceZone04)
        {
            detectedFrequence = 224;

            if (emetteur.GetComponent<EmetteurType>().asAnAnomalie == true)
            {
                anomalieSignal = 3;
            }
            else
            {
                anomalieSignal = 0;
            }
        }
        else if (captedDistance <= distanceZone04 && captedDistance >= distanceZone05)
        {
            detectedFrequence = 267;

            if (emetteur.GetComponent<EmetteurType>().asAnAnomalie == true)
            {
                anomalieSignal = 4;
            }
            else
            {
                anomalieSignal = 0;
            }
        }
        else if (captedDistance <= distanceZone05)
        {
            detectedFrequence = 300;

            if (emetteur.GetComponent<EmetteurType>().asAnAnomalie == true)
            {
                anomalieSignal = 5;
            }
            else
            {
                anomalieSignal = 0;
            }
        }
    }

    void AnomalieLeds()
    {
        if (deviceIsOn && anomalieSignal == 1)
        {
            anomalieLeds[0].GetComponent<ChangeMaterial>().actualMaterial = materials[1];
            anomalieLeds[1].GetComponent<ChangeMaterial>().actualMaterial = materials[0];
            anomalieLeds[2].GetComponent<ChangeMaterial>().actualMaterial = materials[0];
            anomalieLeds[3].GetComponent<ChangeMaterial>().actualMaterial = materials[0];
            anomalieLeds[4].GetComponent<ChangeMaterial>().actualMaterial = materials[0];
        }
        else if (deviceIsOn && anomalieSignal == 2)
        {
            anomalieLeds[0].GetComponent<ChangeMaterial>().actualMaterial = materials[1];
            anomalieLeds[1].GetComponent<ChangeMaterial>().actualMaterial = materials[1];
            anomalieLeds[2].GetComponent<ChangeMaterial>().actualMaterial = materials[0];
            anomalieLeds[3].GetComponent<ChangeMaterial>().actualMaterial = materials[0];
            anomalieLeds[4].GetComponent<ChangeMaterial>().actualMaterial = materials[0];
        }
        else if (deviceIsOn && anomalieSignal == 3)
        {
            anomalieLeds[0].GetComponent<ChangeMaterial>().actualMaterial = materials[1];
            anomalieLeds[1].GetComponent<ChangeMaterial>().actualMaterial = materials[1];
            anomalieLeds[2].GetComponent<ChangeMaterial>().actualMaterial = materials[1];
            anomalieLeds[3].GetComponent<ChangeMaterial>().actualMaterial = materials[0];
            anomalieLeds[4].GetComponent<ChangeMaterial>().actualMaterial = materials[0];
        }
        else if (deviceIsOn && anomalieSignal == 4)
        {
            anomalieLeds[0].GetComponent<ChangeMaterial>().actualMaterial = materials[1];
            anomalieLeds[1].GetComponent<ChangeMaterial>().actualMaterial = materials[1];
            anomalieLeds[2].GetComponent<ChangeMaterial>().actualMaterial = materials[1];
            anomalieLeds[3].GetComponent<ChangeMaterial>().actualMaterial = materials[2];
            anomalieLeds[4].GetComponent<ChangeMaterial>().actualMaterial = materials[0];
        }
        else if (deviceIsOn && anomalieSignal == 5)
        {
            anomalieLeds[0].GetComponent<ChangeMaterial>().actualMaterial = materials[1];
            anomalieLeds[1].GetComponent<ChangeMaterial>().actualMaterial = materials[1];
            anomalieLeds[2].GetComponent<ChangeMaterial>().actualMaterial = materials[1];
            anomalieLeds[3].GetComponent<ChangeMaterial>().actualMaterial = materials[2];
            anomalieLeds[4].GetComponent<ChangeMaterial>().actualMaterial = materials[3];
        }
        else if (deviceIsOn && anomalieSignal == 0)
        {
            anomalieLeds[0].GetComponent<ChangeMaterial>().actualMaterial = materials[0];
            anomalieLeds[1].GetComponent<ChangeMaterial>().actualMaterial = materials[0];
            anomalieLeds[2].GetComponent<ChangeMaterial>().actualMaterial = materials[0];
            anomalieLeds[3].GetComponent<ChangeMaterial>().actualMaterial = materials[0];
            anomalieLeds[4].GetComponent<ChangeMaterial>().actualMaterial = materials[0];
        }
        else
        {
            anomalieLeds[0].GetComponent<ChangeMaterial>().actualMaterial = materials[0];
            anomalieLeds[1].GetComponent<ChangeMaterial>().actualMaterial = materials[0];
            anomalieLeds[2].GetComponent<ChangeMaterial>().actualMaterial = materials[0];
            anomalieLeds[3].GetComponent<ChangeMaterial>().actualMaterial = materials[0];
            anomalieLeds[4].GetComponent<ChangeMaterial>().actualMaterial = materials[0];
        }
    }

    void RebindFrequence()
    {
        //Conditions pour remplir la barre
        if(anomalieSignal == 5 && emetteur.GetComponent<EmetteurType>().asAnAnomalie && emetteur.GetComponent<EmetteurType>().emettorRadio)
        {
            if (rebindBar.valeur == 0)
            {
                textToRepair.SetActive(true);
            }
            else
            {
                textToRepair.SetActive(false);
            }


            if (rebindingFrequence) //Appuie sur input
            {
                if (deviceIsOn)
                {
                    rebindBar.valeur += 5 * Time.deltaTime;
                }
                else if (true)
                {
                    rebindBar.valeur += 5 * Time.deltaTime * 0f;
                }

                textToRepair.SetActive(false);
            }
        }
        else
        {
            textToRepair.SetActive(false);
        }

        //La Barre est pleine
        if (rebindBar.valeur == rebindBar.max)
        {
            rebindingFrequence = false;
            emetteur.GetComponent<EmetteurType>().asAnAnomalie = false;
            rebindBarObject.SetActive(false);
            rebindBar.valeur = 0;
            textIsRepair.SetActive(true);
            StartCoroutine(ChangeFreqState());
        }
    }

    IEnumerator ChangeFreqState()
    {
        yield return new WaitForSeconds(3f);
        textIsRepair.SetActive(false);
        rebindBarObject.SetActive(true);
    }

    public void Teleportation(Transform teleportpoint)
    {
        if (emetteur.GetComponent<EmetteurType>().emmettorTV && emetteur.GetComponent<EmetteurType>().canTeleport)
        {
            if (distance <= 1)
            {
                GameManager.Instance.playerEntity.transform.position = teleportpoint.position;
                GameManager.Instance.isTeleport = true;
            }
        }
    }

    void VoyageTf()
    {
        if (deviceIsOn && voyageTfAvailable)
        {
            if (anomalieSignal == 5 && emetteur.GetComponent<EmetteurType>().asAnAnomalie && emetteur.GetComponent<EmetteurType>().emmettorTV)
            {
                availableLed.GetComponent<ChangeMaterial>().actualMaterial = materials[1];
            }
        }
        else
        {
            availableLed.GetComponent<ChangeMaterial>().actualMaterial = materials[0];
        }

        if (deviceIsOn && !voyageTfAvailable)
        {
            disavailableLed.GetComponent<ChangeMaterial>().actualMaterial = materials[3];
        }
        else
        {
            disavailableLed.GetComponent<ChangeMaterial>().actualMaterial = materials[0];
        }
    }
}
