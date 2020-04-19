using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public enum AudioEffect
{
    babyIdle, // X
    babyLaugh, // X
    babyFall, // X
    balloonPop, // X
    axeSwing, //
    pusherRumble,//
    pusherPush,// 
    lavaSplash, //X
    babyMagnitize,//X (klinkt nog kut)
    resultScreen,//X
    fail,//X
    uiClick,//! behalvem menu
    panMove //X
}
public enum Music
{
    menu,
    level,
    result
}
public class AudioManager : MonoBehaviour
{

    [SerializeField]
    private List<AudioInstance> soundEffectInstances;

    public static AudioManager instance;
    void Awake()
    {
        instance = this;

    }

    public void PlaySound(AudioEffect audioEffect, float volume = -1, bool makeInstace = false)
    {
        AudioInstance selectedAudio = soundEffectInstances.Find(x => x.audioEffect == audioEffect);

        if (makeInstace)
        {
            selectedAudio = new AudioInstance();
            GameObject newObj = Instantiate(selectedAudio.audioS.gameObject);
            selectedAudio.audioS = newObj.GetComponent<AudioSource>();
        }
        selectedAudio.audioS.spatialBlend = 0;
        if (volume != -1)
        {
            selectedAudio.audioS.volume = volume;
        }
        selectedAudio.audioS.Play();
    }


    public void Play3DSound(AudioEffect audioEffect, float volume, Vector3 position, bool makeInstace = false, float pitch = 1)
    {
        AudioInstance selectedAudio = soundEffectInstances.Find(x => x.audioEffect == audioEffect);

        if (makeInstace)
        {
            selectedAudio = new AudioInstance();
            GameObject newObj = Instantiate(selectedAudio.audioS.gameObject);
            selectedAudio.audioS = newObj.GetComponent<AudioSource>();
        }
        selectedAudio.audioS.pitch = pitch;
        selectedAudio.audioS.spatialBlend = 1;
        selectedAudio.audioS.gameObject.transform.position = position;
        selectedAudio.audioS.volume = volume;
        selectedAudio.audioS.Play();
    }
    public void StopSound(AudioEffect audioEffect)
    {
        AudioInstance selectedAudio = soundEffectInstances.Find(x => x.audioEffect == audioEffect);
        selectedAudio.audioS.Stop();
    }
}

[System.Serializable]
public class AudioInstance
{
    public AudioEffect audioEffect;
    public AudioSource audioS;
}
