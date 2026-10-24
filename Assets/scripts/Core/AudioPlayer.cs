using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    private static AudioPlayer instance;

    private AudioSource source;

    private void Awake()
    {
        instance = this;
        source = GetComponent<AudioSource>();
    }

    public static void Play(AudioClip clip)
    {
        if (instance == null || clip == null) return;

        instance.source.PlayOneShot(clip);
    }
}
