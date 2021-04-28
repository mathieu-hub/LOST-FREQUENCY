using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeFrequence : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    public int frequence;

    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        frequence = ModificateurDeFrequence.Instance.detectedFrequence;
    }

    void Update()
    {
        textMesh.text = frequence.ToString();
    }
}
