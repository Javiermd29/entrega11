using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{

    public AudioClip[] musicList;

    private AudioSource source;

    public Text songTitleText;

    private int currentSong;

    void Start()
    {
        source = GetComponent<AudioSource>();

        PlayMusic();
    }

    
    public void PlayMusic()
    {
        if (source.isPlaying)
        {
            return;
        }

        currentSong--;
        if (currentSong<0)
        {
            currentSong = musicList.Length-1;
        }

        StartCoroutine("WaitForMusicEnd");

    }

    IEnumerator WaitForMusicEnd()
    {
        while (source.isPlaying)
        {
            yield return null;
        }
    }

    public void NextSong()
    {
        source.Stop();
        currentSong++;
        if (currentSong > musicList.Length-1)
        {
            currentSong = 0;
        }

        source.clip = musicList[currentSong];
        source.Play();

        ShowSongTitle();

        StartCoroutine("WaitForMusicEnd");

    }

    public void PreviousSong()
    {

        source.Stop();
        currentSong--;
        if (currentSong < 0)
        {
            currentSong = musicList.Length-1;
        }

        source.clip = musicList[currentSong];
        source.Play();

        ShowSongTitle();

        StartCoroutine("WaitForMusicEnd");

    }

    public void StopSong()
    {

        StopCoroutine("WaitForMusicEnd");
        source.Stop();

    }

    void ShowSongTitle()
    {
       songTitleText.text = source.clip.name;
    }

}
