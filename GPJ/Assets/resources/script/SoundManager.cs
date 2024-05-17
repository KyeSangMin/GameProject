using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance = null;


    public AudioSource musicSource; // ��� ���ǿ� AudioSource
    public AudioSource musicSource2; // ��� ���ǿ� AudioSource
    public AudioSource musicSource3; // ��� ���ǿ� AudioSource
    public List<AudioSource> effectsSources; // ȿ������ AudioSource ����Ʈ
    public int maxEffectsSources = 10; // �ִ� AudioSource ��


    private void Awake()
    {
        if (Instance != null)
        {
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // AudioSource ����Ʈ �ʱ�ȭ
        effectsSources = new List<AudioSource>();
        for (int i = 0; i < maxEffectsSources; i++)
        {
            AudioSource newSource = gameObject.AddComponent<AudioSource>();
            effectsSources.Add(newSource);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    // ȿ���� ��� �޼���
    public void PlayEffect(AudioClip clip)
    {
        AudioSource availableSource = GetAvailableSource();
        if (availableSource != null)
        {
            availableSource.PlayOneShot(clip);
        }
    }

    // ��� ������ AudioSource ��������
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

    // ȿ���� ���� ���� �޼���
    public void SetEffectsVolume(float volume)
    {
        foreach (AudioSource source in effectsSources)
        {
            source.volume = volume;
        }
    }

}
