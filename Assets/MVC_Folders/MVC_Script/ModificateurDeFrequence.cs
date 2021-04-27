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

    [Header("VOYAGE TRANSFREQUENTIEL")]
    public bool activeVoyageTf = false;
    public GameObject interruptorVoyageTf;
    public Animator animatorVoyageTf;
    public GameObject ledVoyageTf;
    
    [Header("ANOMALIE DETECTEUR")]
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
    }

    private void Update()
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
}
