using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance = null;

    public GameObject HealEffect;
    public GameObject DefenceEffect;
    public GameObject DamageEffect;
    private void Awake()
    {
        if (Instance != null)
        {
            return;
        }

        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StartEffectAni(int eventNum, Vector2 _position)
    {
        Vector3 spawnallyPos = new Vector3(_position.x, _position.y -0.8f );
        Vector3 spawnenemyPos = new Vector3(_position.x, _position.y);
        switch (eventNum)
        {
            case 1:
                Instantiate(HealEffect, spawnallyPos, transform.rotation);
                break;
            case 2:
                Instantiate(DefenceEffect, spawnenemyPos, transform.rotation);
                break;
            case 3:
                Instantiate(DamageEffect, spawnenemyPos, transform.rotation);
                break;
        }
    }
}
