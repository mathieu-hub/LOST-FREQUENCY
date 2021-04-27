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

    [Header("VOYAGE TRANSFREQUENTIEL")]
    public bool activeVoyageTf = false;
    public GameObject interruptorVoyageTf;
    public Animator animatorVoyageTf;
    

    [Header("ANOMALIE DETECTEUR")]
    public List<GameObject> anomalieLeds;

    [Header("MATERIALS REFERENCES")]
    public List<Material> materials;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        // Activation et Désactivation des interrupteurs
        if (deviceIsOn)
        {
            animatorOnOff.SetBool("switchOn", true);
        }
        else if (!deviceIsOn)
        {
            animatorOnOff.SetBool("switchOn", false);
        }

        if (activeVoyageTf)
        {
            animatorVoyageTf.SetBool("tfSwitchOn", true);
        }
        else if (!activeVoyageTf)
        {
            animatorVoyageTf.SetBool("tfSwitchOn", false);
        }
    }
}
