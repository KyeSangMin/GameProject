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

    private HPController hpController;

    void Start()
    {
        hpController = gameObject.GetComponentInChildren<HPController>();

        Range = 2;
        Speed = 100;
        MaxHP = 30;
        CurrentHP = MaxHP;
        Damage = 3;
        //position = new Vector2(0, 0);
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public int getSpeed()
    {

        int random = Random.Range(10, 30);
        return Speed + random; 
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
        hpController.updateHPText(CurrentHP);
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

    public int getMaxHP()
    {
        return MaxHP;
    }

}
