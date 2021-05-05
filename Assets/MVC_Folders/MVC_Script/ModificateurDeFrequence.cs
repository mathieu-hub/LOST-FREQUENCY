using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ThunderWire.CrossPlatform.Input;

public class ModificateurDeFrequence : MonoBehaviour
{
    public static ModificateurDeFrequence Instance;
    private CrossPlatformInput crossPlatformInput;

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
    public bool voyageTfAvailable = false; // Variable � changer 
    public GameObject availableLed;
    public GameObject disavailableLed;

    [Header("ANOMALIE DETECTEUR")]
    public int anomalieSignal; // Variable � changer
    public List<GameObject> anomalieLeds = new List<GameObject>();

    [Header("ECRAN")]
    public GameObject indicateurFrequence;
    private TextMeshProUGUI textMesh;
    public int detectedFrequence;

    [Header("MATERIALS REFERENCES")]
    public List<Material> materials = new List<Material>();

    [Header("INPUTS")]
    private bool useOnOff;
    private bool useVoyageTf;
    private bool useRebindFreq;

    private void Awake()
    {
        Instance = this;
        crossPlatformInput = CrossPlatformInput.Instance;
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
        if (crossPlatformInput.inputsLoaded)
        {
            useOnOff = crossPlatformInput.GetInput<bool>("MFOnOff");
        }

        InterruptorOnOff();
        InterruptorVoyageTf();
        AnomalieLeds();
        VoyageTf();
    }

    

    void InterruptorOnOff()
    {
        if (useOnOff)
        {
            // Activation et D�sactivation des interrupteurs
            if (deviceIsOn) // Allumage du bo�tier
            {
                animatorOnOff.SetBool("switchOn", true);
                ledOnOff.GetComponent<ChangeMaterial>().actualMaterial = materials[1];
            }
            else if (!deviceIsOn) // Extinction du bo�tier
            {
                animatorOnOff.SetBool("switchOn", false);
                ledOnOff.GetComponent<ChangeMaterial>().actualMaterial = materials[0];

                if (activeVoyageTf)
                {
                    activeVoyageTf = false;
                }
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
