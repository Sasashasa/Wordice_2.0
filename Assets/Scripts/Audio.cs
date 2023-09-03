using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Audio : MonoBehaviour
{
    [Header("Music")]
    [SerializeField] private AudioClip _russiaTheme;
    [SerializeField] private AudioClip _africaTheme;
    [SerializeField] private AudioClip _japanTheme;
    [SerializeField] private AudioClip _transylvaniaTheme;
    [SerializeField] private AudioClip _usaTheme;
    
    [Header("Sounds")]
    [SerializeField] private AudioClip _throw;
    [SerializeField] private AudioClip _spin;
    [SerializeField] private AudioClip _win;

    [Header("Components")] 
    [SerializeField] private LevelGenerator _levelGenerator;
    
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        int levelId = _levelGenerator.CurLevel.Id;

        if (levelId < 10)
        {
            _audioSource.clip = _russiaTheme;
        }
        else if (levelId < 20)
        {
            _audioSource.clip = _africaTheme;
        }
        else if (levelId < 30)
        {
            _audioSource.clip = _japanTheme;
        }
        else if (levelId < 40)
        {
            _audioSource.clip = _transylvaniaTheme;
        }
        else if (levelId < 50)
        {
            _audioSource.clip = _usaTheme;
        }
        
        _audioSource.Play();
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