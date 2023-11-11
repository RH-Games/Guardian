using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc : MonoBehaviour
{
    public List<AudioClip> GameMusic;
    public List<AudioClip> ambientSounds;

    public AudioSource audioSource;


    public int MusicPos;
    public int SoundPos;
    // Start is called before the first frame update
    void Start()
    {
      playMusic();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void playMusic()
    {
        MusicPos = (int)Mathf.Floor(Random.Range(0, GameMusic.Count));
        audioSource.PlayOneShot(GameMusic[MusicPos]);

    }

    public void playAmbient()
    {
        SoundPos = (int)Mathf.Floor(Random.Range(0, ambientSounds.Count));
       // audioSource.Play(ambientSounds[SoundPos]);
        //audioSource.PlayOneShot(GameMusic[MusicPos]);

    }

}
