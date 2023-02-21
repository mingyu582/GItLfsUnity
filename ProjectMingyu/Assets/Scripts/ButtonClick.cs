using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    public AudioClip clip;
    public void ClickButtonSound()
    {
        SoundManager.instance.SFXPlay("ClickButton", clip);
    }
}
