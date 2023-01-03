using System.Collections;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public AudioClip[] sounds;
    private AudioSource audioSource;
    private AudioClip clip;

    private int tempSongNum;
    private int currentSongNum;

    public static AudioManager instance;
    [SerializeField] KeyCode nextKey = KeyCode.N;
    [SerializeField] KeyCode stopKey = KeyCode.M;

    bool isEnabled = true;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } 
        else 
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        
    }

    void Start()
    {
        audioSource = FindObjectOfType<AudioSource>();
        audioSource.loop = false;
    }

    void StopSong()
    {   
        if (audioSource.isPlaying) 
                audioSource.Stop();
        isEnabled = false;
    }
    void NextSong()
    {
        if (audioSource.isPlaying) 
            audioSource.Stop();

        currentSongNum = UnityEngine.Random.Range(0, sounds.Length);

        if (currentSongNum != tempSongNum)
        {
            tempSongNum = currentSongNum;
            clip = sounds[currentSongNum];
            audioSource.PlayOneShot(clip);
            isEnabled = true;
        }
        else if (currentSongNum == tempSongNum)
        {
            clip = sounds[UnityEngine.Random.Range(0, sounds.Length)];
            audioSource.PlayOneShot(clip);
            isEnabled = true;
        }
        
    }

    void Update()
    {
        if (!audioSource.isPlaying && isEnabled)
        {  
            NextSong();
        }

        if (Input.GetKeyDown(stopKey))
        {
            StopSong();
        }

        if (Input.GetKeyDown(nextKey))
        {
            NextSong();
        }
    }

}
