using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeFrequence : MonoBehaviour
{
    public TextMeshPro textMesh;
    public int frequence;

    void Start()
    {       
        frequence = ModificateurDeFrequence.Instance.detectedFrequence;
    }

    void Update()
    {
        frequence = ModificateurDeFrequence.Instance.detectedFrequence;
        textMesh.text = frequence.ToString();
    }
}
