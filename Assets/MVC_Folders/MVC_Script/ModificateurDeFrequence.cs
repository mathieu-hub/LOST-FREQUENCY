using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public bool voyageTfAvailable = false;
    public GameObject availableLed;
    public GameObject disavailableLed;

    [Header("ANOMALIE DETECTEUR")]
    public int anomalieSignal;
    public List<GameObject> anomalieLeds = new List<GameObject>();

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


        anomalieSignal = 0;        
    }

    private void Update()
    {
        Interruptor();
        AnomalieLeds();
        VoyageTf();
    }

    void Interruptor()
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
            anomalieSignal = 0;

            if (activeVoyageTf)
            {
                activeVoyageTf = false;
            }
        }

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

    void AnomalieLeds()
    {
        if (anomalieSignal == 0)
        {
            anomalieLeds[0].GetComponent<ChangeMaterial>().actualMaterial = materials[0];
            anomalieLeds[1].GetComponent<ChangeMaterial>().actualMaterial = materials[0];
            anomalieLeds[2].GetComponent<ChangeMaterial>().actualMaterial = materials[0];
            anomalieLeds[3].GetComponent<ChangeMaterial>().actualMaterial = materials[0];
            anomalieLeds[4].GetComponent<ChangeMaterial>().actualMaterial = materials[0];
        }
        else if (anomalieSignal == 1)
        {
            anomalieLeds[0].GetComponent<ChangeMaterial>().actualMaterial = materials[1];
            anomalieLeds[1].GetComponent<ChangeMaterial>().actualMaterial = materials[0];
            anomalieLeds[2].GetComponent<ChangeMaterial>().actualMaterial = materials[0];
            anomalieLeds[3].GetComponent<ChangeMaterial>().actualMaterial = materials[0];
            anomalieLeds[4].GetComponent<ChangeMaterial>().actualMaterial = materials[0];
        }
        else if (anomalieSignal == 2)
        {
            anomalieLeds[0].GetComponent<ChangeMaterial>().actualMaterial = materials[1];
            anomalieLeds[1].GetComponent<ChangeMaterial>().actualMaterial = materials[1];
            anomalieLeds[2].GetComponent<ChangeMaterial>().actualMaterial = materials[0];
            anomalieLeds[3].GetComponent<ChangeMaterial>().actualMaterial = materials[0];
            anomalieLeds[4].GetComponent<ChangeMaterial>().actualMaterial = materials[0];
        }
        else if (anomalieSignal == 3)
        {
            anomalieLeds[0].GetComponent<ChangeMaterial>().actualMaterial = materials[1];
            anomalieLeds[1].GetComponent<ChangeMaterial>().actualMaterial = materials[1];
            anomalieLeds[2].GetComponent<ChangeMaterial>().actualMaterial = materials[1];
            anomalieLeds[3].GetComponent<ChangeMaterial>().actualMaterial = materials[0];
            anomalieLeds[4].GetComponent<ChangeMaterial>().actualMaterial = materials[0];
        }
        else if (anomalieSignal == 4)
        {
            anomalieLeds[0].GetComponent<ChangeMaterial>().actualMaterial = materials[1];
            anomalieLeds[1].GetComponent<ChangeMaterial>().actualMaterial = materials[1];
            anomalieLeds[2].GetComponent<ChangeMaterial>().actualMaterial = materials[1];
            anomalieLeds[3].GetComponent<ChangeMaterial>().actualMaterial = materials[2];
            anomalieLeds[4].GetComponent<ChangeMaterial>().actualMaterial = materials[0];
        }
        else if (anomalieSignal == 5)
        {
            anomalieLeds[0].GetComponent<ChangeMaterial>().actualMaterial = materials[1];
            anomalieLeds[1].GetComponent<ChangeMaterial>().actualMaterial = materials[1];
            anomalieLeds[2].GetComponent<ChangeMaterial>().actualMaterial = materials[1];
            anomalieLeds[3].GetComponent<ChangeMaterial>().actualMaterial = materials[2];
            anomalieLeds[4].GetComponent<ChangeMaterial>().actualMaterial = materials[3];
        }
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
