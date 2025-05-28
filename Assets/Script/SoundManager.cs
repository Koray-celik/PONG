using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _goal;
    [SerializeField] private AudioClip _goalAgainst;
    [SerializeField] private AudioClip _racketHit;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void PlayGoal() => _audioSource.PlayOneShot(_goal);
    public void PlayGoalAgainst() => _audioSource.PlayOneShot(_goalAgainst);
    public void PlayRacketHit() => _audioSource.PlayOneShot(_racketHit);
}
