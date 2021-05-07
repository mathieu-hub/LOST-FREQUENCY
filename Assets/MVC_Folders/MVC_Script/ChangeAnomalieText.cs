using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeAnomalieText : MonoBehaviour
{
    public TextMeshPro textMesh;
    public string anomalieDisplay;

    void Start()
    {
        anomalieDisplay = ModificateurDeFrequence.Instance.detectedAnomalie;
    }

    void Update()
    {
        anomalieDisplay = ModificateurDeFrequence.Instance.detectedAnomalie;
        textMesh.text = anomalieDisplay;
    }
}
