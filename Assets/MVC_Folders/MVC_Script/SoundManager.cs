using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("AMBIANCES")]
    public AK.Wwise.Event ambianceLightning;
    public AK.Wwise.Event anomalieRadio;
    public AK.Wwise.Event CloseThunder;
    public AK.Wwise.Event horrorLoopSalvation;
    public AK.Wwise.Event vhsTape;

    [Header("TERENCE DIALOGUES")]
    public AK.Wwise.Event Terence01;
    public AK.Wwise.Event Terence02;
    public AK.Wwise.Event Terence03;
    public AK.Wwise.Event Terence04;
    public AK.Wwise.Event Terence05;
    public AK.Wwise.Event Terence06;
    public AK.Wwise.Event Terence07;

    [Header("ARON DIALOGUES")]
    public AK.Wwise.Event Aron01;
    public AK.Wwise.Event Aron02;
    public AK.Wwise.Event Aron03;
    public AK.Wwise.Event Aron04;
    public AK.Wwise.Event Aron05;
    public AK.Wwise.Event Aron06;
    public AK.Wwise.Event Aron07;

    [Header("DARK ROOM VOICES")]
    public AK.Wwise.Event[] voicesDarkRoom;
    
    [Header("SFX")]
    public AK.Wwise.Event sciFiStinger;



    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
