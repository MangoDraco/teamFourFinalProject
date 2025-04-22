using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScene : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip introDialogue;
    SceneManager sceneManager;
    // Start is called before the first frame update
    void Start()
    {
        PlaySoundOnce(introDialogue);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sceneManager.LoadNextScene("howToPlay");
        }

        if (!audioSource.isPlaying)
        {
            sceneManager.LoadNextScene("howToPlay");
        }
    }

    void PlaySoundOnce(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
    
}
