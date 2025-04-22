using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScene : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip endDialogue;
    // Start is called before the first frame update
    void Start()
    {
        PlaySoundOnce(endDialogue);
    }

    // Update is called once per frame
    void PlaySoundOnce(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
