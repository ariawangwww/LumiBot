using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] crashEffects; // 存放音效的数组
    public AudioClip checkEffect;
    public AudioClip needleEffect;
    public AudioClip chaseEffect;
    public AudioClip growlEffect;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayRandomSound()
    {
        if (crashEffects.Length > 0)
        {
            int randomIndex = Random.Range(0, crashEffects.Length);
            audioSource.PlayOneShot(crashEffects[randomIndex]);
        }
    }

    public void PlayCheckSound()
    {
        audioSource.PlayOneShot(checkEffect);
    }

    public void PlayNeedleSound()
    {
        audioSource.PlayOneShot(needleEffect);
    }

    public void PlayChaseSound()
    {
        audioSource.PlayOneShot(chaseEffect);
    }

    public void PlayGrowlSound()
    {
        audioSource.PlayOneShot(growlEffect);
    }
}
