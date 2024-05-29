using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField]
    private int Range;
    [SerializeField]
    private int Speed;
    [SerializeField]
    private int MaxHP;
    [SerializeField]
    private int CurrentHP;
    [SerializeField]
    private int Damage;
    [SerializeField]
    private Vector2 position;

    public int TypeNum;


    [SerializeField]
    public enum CharacterType {
        Aggressive,
        Defensive,
        Agile

    }
    [System.Serializable]
    public struct PlayerStats
    {
        
        public int health;

        public int maxhealth;

        public int attackDamage;

        public int range;

        public int speed;

        public float defence;
        // 생성자 정의
        public PlayerStats(int health, int maxhealth, int attackDamage, int range, int speed, float defence)
        {
            this.health = health;
            this.maxhealth = maxhealth;
            this.attackDamage = attackDamage;
            this.range = range;
            this.speed = speed;
            this.defence = defence;
        }
    }

    public CharacterType characterType;
    private PlayerStats playerStats;

    private void Awake()
    {
        playerStats.health = 100;
    }

    void Start()
    {


        /*int health, int maxhealth, int attackDamage, int range, int speed*/
        switch (TypeNum)
        {
            case 1:
                playerStats = new PlayerStats(200, 200, 100, 2, 105, 50); // aggressive
                break;
            case 2:
                playerStats = new PlayerStats(300, 300, 50, 2, 100, 100); // defensive
                break;
            case 3:
                playerStats = new PlayerStats(150, 150, 75, 3, 130, 25); //aglie
                break;
            case 4:
                int random = Random.Range(93, 97);
                playerStats = new PlayerStats(200, 200, 50, 2, random, 51);
                break;
            case 5:
                playerStats = new PlayerStats(200, 100, 25, 2, 107, 50);
                break;
            case 6:
                playerStats = new PlayerStats(100, 100, 50, 3, 115, 100);
                break;
            case 7:
                playerStats = new PlayerStats(1000, 1000, 1000, 5, 205, 100);
                break;

        }

        
        Damage = playerStats.attackDamage;
    }


    // Update is called once per frame
    void Update()
    {
        CurrentHP = playerStats.health;
        MaxHP = playerStats.maxhealth;
        //Damage = playerStats.attackDamage;
    }

    public int getSpeed()
    {

        int random = Random.Range(10, 30);
        return playerStats.speed + random; 
    }

    public int getRange()
    {
        return playerStats.range;
    }
     
    public void setCharacterPos(Vector2 inputpostion)
    {
        position = inputpostion;
    }

    public Vector2 getCharacterPos()
    {
        return position;
    }

    public int damageCalculate(int damage)
    {

        float Caldamage;
        float typeBonus = 1.0f;
        switch(characterType)
        {
            case CharacterType.Aggressive:
                typeBonus = 1.25f;
                break;
            case CharacterType.Agile:
                typeBonus = 0.9f;
                break;
            case CharacterType.Defensive:
                typeBonus = 0.75f;
                break;
        }
        float declineRate = 1 - this.playerStats.defence / 500;
        Caldamage = damage * typeBonus * declineRate;
        

        return (int)Caldamage;
    }

    public void setHP(bool IsAttack, int damage)
    {
        if(!IsAttack)
        {
            return;
        }
        playerStats.health = playerStats.health - damageCalculate(damage);
        IsAttack = false;
    }

    public void EventSetHP(int eventnum, int value)
    {
        switch(eventnum)
        {
            case 1:
                int temphp = playerStats.health + value;
                if(temphp >= playerStats.maxhealth)
                {
                    playerStats.health = playerStats.maxhealth;
                }
                break;
            case 2:
                playerStats.health = playerStats.health - value;
                break;
        }
        
    }

    public void EventSetDefence()
    {
        playerStats.defence = 0;
    }
    public int getDamage()
    {
        return playerStats.attackDamage;
    }

    public int getCurrentHP()
    {
        return playerStats.health;
    }

    public int getMaxHP()
    {
        return playerStats.maxhealth;
    }

}
