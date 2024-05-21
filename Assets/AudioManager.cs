using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Button btnOn;
    public Button btnOff;

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

        btnOn.onClick.AddListener(() => PlayAudio());
        btnOff.onClick.AddListener(() => StopAudio());

        audioSource = GameObject.Find("bg").GetComponent<AudioSource>();
    }

    void PlayAudio()
    {
        audioSource.volume = 0.5f;
    }

    void StopAudio()
    {
        audioSource.volume = 0f;
    }

    public void PlaySFX (AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
