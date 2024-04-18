using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{

    public AudioClip[] musicList;

    public Sprite[] musicImages;

    private AudioSource source;

    [SerializeField] private Image songImage;

    [SerializeField] private TMPro.TextMeshProUGUI musicTitle;

    private int currentSong = 0;

    private bool randomOnOf;

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
        if (currentSong < 0)
        {
            currentSong = musicList.Length - 1;
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

        if (!randomOnOf)
        {
            currentSong++;
            if (currentSong > musicList.Length - 1)
            {
                currentSong = 0;
            }
        }
        else
        {
            currentSong = Random.Range(0, musicList.Length);
        }
        

        source.clip = musicList[currentSong];

        songImage.sprite = musicImages[currentSong];

        musicTitle.text = musicList[currentSong].name;

        source.Play();



        StartCoroutine("WaitForMusicEnd");

    }

    public void PreviousSong()
    {

        source.Stop();
        currentSong--;
        if (currentSong < 0)
        {
            currentSong = musicList.Length - 1;
        }

        source.clip = musicList[currentSong];

        songImage.sprite = musicImages[currentSong];

        musicTitle.text = musicList[currentSong].name;

        source.Play();

        StartCoroutine("WaitForMusicEnd");

    }

    public void StopSong()
    {

        StopCoroutine("WaitForMusicEnd");
        source.Stop();

    }

    public void LoopSong()
    {

        if (source.loop)
        {
            source.loop = false;
        }
        else
        {
            source.loop = true;
        }

    }

    public void RandomSong()
    {
        if (!randomOnOf)
        {
            currentSong = Random.Range(0, musicList.Length);
            source.clip = musicList[currentSong];
            songImage.sprite = musicImages[currentSong];
            musicTitle.text = musicList[currentSong].name;
            source.Play();
            randomOnOf = true;
        }
        else
        {
            randomOnOf = false;
        }

        

    }

}