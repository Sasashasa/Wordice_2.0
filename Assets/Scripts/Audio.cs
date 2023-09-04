using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Audio : MonoBehaviour
{
    [Header("Sounds")]
    [SerializeField] private AudioClip _throw;
    [SerializeField] private AudioClip _spin;
    [SerializeField] private AudioClip _win;
    
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySpinSound() 
    {
        _audioSource.PlayOneShot(_spin);
    }

    public void PlayWinSound() 
    {
        _audioSource.PlayOneShot(_win);
    }

    public void PlayThrowSound() 
    {
        StartCoroutine(WaitForThrow());
    }

    private IEnumerator WaitForThrow() 
    {
        yield return new WaitForSeconds(0.3f);
        _audioSource.PlayOneShot(_throw);
    }
}