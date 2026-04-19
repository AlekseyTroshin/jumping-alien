using UnityEngine;

public class SoundEffecter : MonoBehaviour
{
    
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _jumpSound, _cointSound, _winSound, _loseSound;

    public void PlayJumpSound()
    {
        _audioSource.PlayOneShot(_jumpSound);
    }

    public void PlayCointSound()
    {
        _audioSource.PlayOneShot(_cointSound);
    }

    public void PlayWinSound()
    {
        _audioSource.PlayOneShot(_winSound);
    }

    public void PlayLoseSound()
    {
        _audioSource.PlayOneShot(_loseSound);
    }

}
