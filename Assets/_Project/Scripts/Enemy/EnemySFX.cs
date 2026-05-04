using UnityEngine;

public class EnemySFX : MonoBehaviour
{
    private AudioSource _zombieSFX;

    void Awake()
    {
        _zombieSFX = GetComponentInChildren<AudioSource>();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;

        _zombieSFX.PlayOneShot(clip);
    }
}
