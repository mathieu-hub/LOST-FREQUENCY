using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ThunderWire.CrossPlatform.Input;

public class ModificateurDeFrequence : MonoBehaviour
{
    public static ModificateurDeFrequence Instance;

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
    
    [Space (10)]
    public GameObject recepteur;
    public GameObject emetteur;

    public float distance;
    
    [Space(10)]
    public float distanceZone01;
    public float distanceZone02;
    public float distanceZone03;
    public float distanceZone04;
    public float distanceZone05;
       
    [Header("ECRAN")]
    public GameObject indicateurFrequence;
    private TextMeshProUGUI textMesh;
    public int detectedFrequence;

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
    }

    private void Update()
    {        
        InterruptorOnOff();
        InterruptorVoyageTf();
        DetectEmetteurAnomalie();
        AnomalieLeds();
        VoyageTf();
    }

    

    void InterruptorOnOff()
    {
        // Activation et Désactivation des interrupteurs
        if (deviceIsOn) // Allumage du boîtier
        {
            animatorOnOff.SetBool("switchOn", true);
            ledOnOff.GetComponent<ChangeMaterial>().actualMaterial = materials[1];
        }
        else if (!deviceIsOn) // Extinction du boîtier
        {
            animatorOnOff.SetBool("switchOn", false);
            ledOnOff.GetComponent<ChangeMaterial>().actualMaterial = materials[0];

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

    void DetectEmetteurAnomalie()
    {
        distance = Vector3.Distance(recepteur.transform.position, emetteur.transform.position);


        if (distance >= distanceZone01)
        {
            anomalieSignal = 0;
        }
        else if (distance <= distanceZone01 && distance >= distanceZone02)
        {
            anomalieSignal = 1;
        }
        else if (distance <= distanceZone02 && distance >= distanceZone03)
        {
            anomalieSignal = 2;
        }
        else if (distance <= distanceZone03 && distance >= distanceZone04)
        {
            anomalieSignal = 3;
        }
        else if (distance <= distanceZone04 && distance >= distanceZone05)
        {
            anomalieSignal = 4;
        }
        else if (distance <= distanceZone05)
        {
            anomalieSignal = 5;
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
        
    }


    void VoyageTf()
    {
        if (deviceIsOn && voyageTfAvailable)
        {
            availableLed.GetComponent<ChangeMaterial>().actualMaterial = materials[1];
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
