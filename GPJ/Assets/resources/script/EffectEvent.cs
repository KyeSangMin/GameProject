using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectEvent : MonoBehaviour
{
    SoundManager soundManager;

    // Start is called before the first frame update
    void Start()
    {
        soundManager = SoundManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlaySFX(int sfxNum)
    {

        AudioSource source = soundManager.sfxSorce.transform.GetChild(sfxNum).GetComponent<AudioSource>();
        soundManager.PlayEffect(source.clip);
    }

    public void EndEffect()
    {
        Destroy(this.gameObject);
    }
}
