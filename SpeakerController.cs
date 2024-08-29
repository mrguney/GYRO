using UnityEngine;

public class SpeakerController : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] audioClips; // Şarkıların listesi
    private int currentClipIndex = -1;
    private bool isPaused = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.playOnAwake = false;
        audioSource.Stop();
    }

    void Update()
    {
        // Şarkı bittiğinde bir sonraki şarkıyı çal
        if (!audioSource.isPlaying && !isPaused && currentClipIndex != -1)
        {
            PlayNextSong();
        }
    }

    void OnMouseDown()
    {
        if (audioSource != null)
        {
            if (audioSource.isPlaying)
            {
                PauseSong();
            }
            else
            {
                if (isPaused)
                {
                    ResumeSong();
                }
                else
                {
                    PlayRandomSong();
                }
            }
        }
    }

    void PlayRandomSong()
    {
        if (audioClips.Length > 0)
        {
            int newClipIndex = UnityEngine.Random.Range(0, audioClips.Length);

            // Aynı şarkıyı tekrar çalmamak için kontrol
            while (newClipIndex == currentClipIndex && audioClips.Length > 1)
            {
                newClipIndex = UnityEngine.Random.Range(0, audioClips.Length);
            }

            currentClipIndex = newClipIndex;
            audioSource.clip = audioClips[currentClipIndex];
            audioSource.Play();
            isPaused = false;
        }
        else
        {
            Debug.LogError("AudioClips list is empty!");
        }
    }

    public void PlayNextSong()
    {
        if (audioClips.Length > 0)
        {
            currentClipIndex = (currentClipIndex + 1) % audioClips.Length;
            audioSource.clip = audioClips[currentClipIndex];
            audioSource.Play();
            isPaused = false;
        }
    }

    public void PlayPreviousSong()
    {
        if (audioClips.Length > 0)
        {
            currentClipIndex = (currentClipIndex - 1 + audioClips.Length) % audioClips.Length;
            audioSource.clip = audioClips[currentClipIndex];
            audioSource.Play();
            isPaused = false;
        }
    }

    public void StopPlaying()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
            currentClipIndex = -1; // Şarkı listesini sıfırla
            isPaused = false;
        }
    }

    public void PauseSong()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
            isPaused = true;
        }
    }

    public void ResumeSong()
    {
        if (isPaused && audioSource.clip != null)
        {
            audioSource.UnPause();
            isPaused = false;
        }
    }
}