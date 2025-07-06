using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource coinAudioSource;

    public void PlayCoinSound()
    {
        coinAudioSource.Play();
    }
}