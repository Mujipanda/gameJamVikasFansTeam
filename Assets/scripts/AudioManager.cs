using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Button btnOn;
    public Button btnOff;

    public AudioListener listener;

    public AudioClip bg;
    public AudioClip Missile;

    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    private AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        musicSource.clip = bg;
        musicSource.Play();

        btnOn = GetComponent<Button>();
        btnOff = GetComponent<Button>();

       // btnOn.onClick.AddListener(() => PlayAudio());
       // btnOff.onClick.AddListener(() => StopAudio());

       // audioSource = GameObject.Find("bg").GetComponent<AudioSource>();
    }

     public void PlayAudio()
    {
        listener.enabled = true;
       // audioSource.volume = 0.5f;
        print("PlayAudio");
    }

    public void StopAudio()
    {
       // audioSource.volume = 0f;
        listener.enabled = false;
        print("StopAudio");
    }

    public void PlaySFX (AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
