using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    private int Range;
    private int Speed;
    private int MaxHP;
    private int CurrentHP;
    private int Damage;
    [SerializeField]
    private Vector2 position;


    // Start is called before the first frame update
    void Start()
    {
        Range = 1;
        Speed = 100;
        MaxHP = 100;
        CurrentHP = MaxHP;
        Damage = 100;
        //position = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getSpeed()
    {
        return Speed;
    }

    public int getRange()
    {
        return Range;
    }

    public void setCharacterPos(Vector2 inputpostion)
    {
        position = inputpostion;
    }

    public Vector2 getCharacterPos()
    {
        return position;
    }

    public void setHP(int damage)
    {
        CurrentHP = CurrentHP - damage;
    }
    public int getDamage()
    {
        return Damage;
    }

}
