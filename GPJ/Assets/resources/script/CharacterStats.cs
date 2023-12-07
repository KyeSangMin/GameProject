using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField]
    private int Range;
    private int Speed;
    private int MaxHP;
    [SerializeField]
    private int CurrentHP;
    [SerializeField]
    private int Damage;
    [SerializeField]
    private Vector2 position;



    void Start()
    {
        Range = 3;
        Speed = 100;
        MaxHP = 100;
        CurrentHP = MaxHP;
        Damage = 50;
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

    public void setHP(bool IsAttack, int damage)
    {
        if(!IsAttack)
        {
            return;
        }
        CurrentHP = CurrentHP - damage;
        IsAttack = false;
    }
    public int getDamage()
    {
        return Damage;
    }

    public int getCurrentHP()
    {
        return CurrentHP;
    }

}
