using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RebindBar : MonoBehaviour
{
    static Image Barre;

    public float max { get; set; }

    private float _valeur;
    public float valeur
    {
        get { return _valeur; }
        
        set
        {
            _valeur = Mathf.Clamp(value, 0, max);
            Barre.fillAmount = (1 / max) * _valeur;
        }
    }

    void Start()
    {
        Barre = GetComponent<Image>();
    }
}
