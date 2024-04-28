using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Reference to the AudioSource component
    private static AudioSource audioSource;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // This method plays a given audio clip
    public static void PlayZombieGrunt(AudioClip gruntClip)
    {
        if (gruntClip != null)
        {
            audioSource.PlayOneShot(gruntClip);
        }
        else
        {
            Debug.LogWarning("AudioClip is null");
        }
    }

    public static void PlayZombieHurt(AudioClip hurtClip)
    {
        if (hurtClip != null)
        {
            audioSource.PlayOneShot(hurtClip);
        }
        else
        {
            Debug.LogWarning("AudioClip is null");
        }
    }

    public static void PlayBarrelExplosion(AudioClip explosionClip)
    {
        if (explosionClip != null)
        {
            audioSource.PlayOneShot(explosionClip);
        }
        else
        {
            Debug.LogWarning("AudioClip is null");
        }
    }
}
