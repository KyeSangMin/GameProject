using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance = null;

    public GameObject sfxSorce;

    public AudioSource musicSource; // 배경 음악용 AudioSource
    public AudioSource musicSource2; // 배경 음악용 AudioSource
    public AudioSource musicSource3; // 배경 음악용 AudioSource
    public AudioSource musicSource4; // 배경 음악용 AudioSource
    public AudioSource musicSource5; // 배경 음악용 AudioSource
    public List<AudioSource> effectsSources; // 효과음용 AudioSource 리스트
    public int maxEffectsSources = 10; // 최대 AudioSource 수


    private void Awake()
    {
        if (Instance != null)
        {
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        //EffectsSources[ ] = sfxSorce.GetComponentsInChildren<AudioSource>(true);
        // AudioSource 리스트 초기화
        effectsSources = new List<AudioSource>();
        for (int i = 0; i < maxEffectsSources; i++)
        {
            AudioSource newSource = sfxSorce.transform.GetChild(i).GetComponent<AudioSource>();
            effectsSources.Add(newSource);
        }
        
    }

    void Start()
    {
        SetMusicVolume(0.25f);
        SetEffectsVolume(0.35f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayMusic(int num)
    {
        switch(num)
        {
            case 1:
                musicSource.clip = musicSource.clip;
                break;
            case 2:
                musicSource.clip = musicSource2.clip;
                break;
            case 3:
                musicSource.clip = musicSource3.clip;
                break;
        }
        musicSource.Play();

    }

    public void PauseMusic()
    {
        musicSource.Pause();
    }

    // 효과음 재생 메서드
    public void PlayEffect(AudioClip clip)
    {
        AudioSource availableSource = GetAvailableSource();
        if (availableSource != null)
        {
            availableSource.PlayOneShot(clip);
        }
    }

    // 사용 가능한 AudioSource 가져오기
    private AudioSource GetAvailableSource()
    {
        foreach (AudioSource source in effectsSources)
        {
            if (!source.isPlaying)
            {
                return source;
            }
        }
        return null;
    }


    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    // 효과음 볼륨 설정 메서드
    public void SetEffectsVolume(float volume)
    {
        foreach (AudioSource source in effectsSources)
        {
            source.volume = volume;
        }
    }

}
