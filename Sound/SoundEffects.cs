using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public List<AudioClip> WalkSounds;
    public List<AudioClip> AttackSounds;
    public AudioClip hitSound;
    public AudioSource AudioSource;

    public int AttackPos;
    public int pos;

    //plays walking sounds when called in events in animations.
    public void playSound() {
        pos = (int)Mathf.Floor(Random.Range(0,WalkSounds.Count));
        AudioSource.PlayOneShot(WalkSounds[pos]);
    }


    //plays attack sounds when called 
    public void attackSounds()
    {
        AttackPos = (int)Mathf.Floor(Random.Range(0, AttackSounds.Count));

        AudioSource.PlayOneShot(AttackSounds[AttackPos]);

    }

    public void HitSound()
    {
        AudioSource.PlayOneShot(hitSound);
    }
}
