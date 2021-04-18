﻿using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class ExamineObjectAnimation : MonoBehaviour, ISaveable
{
    public enum animMode { Once, Normal }

    public Animation m_animation;
    public animMode animModes = animMode.Normal;
    public bool isEnabled = true;

    public string useAnimation;
    public string backAnimation;

    private bool isPlayed = false;

    public void EnableScript(bool enable)
    {
        isEnabled = enable;
    }

    public void PlayAnimation()
    {
        if (!m_animation.isPlaying)
        {
            if (animModes == animMode.Normal)
            {
                if (!isPlayed)
                {
                    m_animation.Play(useAnimation);
                    isPlayed = true;
                }
                else
                {
                    m_animation.Play(backAnimation);
                    isPlayed = false;
                }
            }
            else
            {
                if (!isPlayed)
                {
                    m_animation.Play(useAnimation);
                    isPlayed = true;
                }
            }
        }
    }

    public Dictionary<string, object> OnSave()
    {
        return new Dictionary<string, object>()
        {
            { "isPlayed", isPlayed },
            { "rotation", transform.eulerAngles }
        };
    }

    public void OnLoad(JToken token)
    {
        isPlayed = token["isPlayed"].ToObject<bool>();
        transform.eulerAngles = token["rotation"].ToObject<Vector3>();
    }
}
