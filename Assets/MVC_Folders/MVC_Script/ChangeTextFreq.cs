using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeTextFreq : MonoBehaviour
{
    public TextMeshPro textMesh;
    public string textFrequence;

    void Start()
    {
        textFrequence = ModificateurDeFrequence.Instance.frequenceState;
    }

    void Update()
    {
        textFrequence = ModificateurDeFrequence.Instance.frequenceState;
        textMesh.text = textFrequence;
    }
}
