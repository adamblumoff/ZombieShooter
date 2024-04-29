using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDoesntChange : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource audioSource;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if(!audioSource.isPlaying)
            DontDestroyOnLoad(audioSource);
    }
}
