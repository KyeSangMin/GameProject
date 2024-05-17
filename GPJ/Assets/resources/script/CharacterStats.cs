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


    public enum CharacterType {
        Aggressive,
        Defensive,
        Agile

    }
    [SerializeField]
    public struct PlayerStats
    {
        [SerializeField]
        public int health;
        [SerializeField]
        public int maxhealth;
        public int attackDamage;
        public int range;
        public int speed;

        // 생성자 정의
        public PlayerStats(int health, int maxhealth, int attackDamage, int range, int speed)
        {
            this.health = health;
            this.maxhealth = maxhealth;
            this.attackDamage = attackDamage;
            this.range = range;
            this.speed = speed;
        }
    }

    private PlayerStats playerStats;

    private void Awake()
    {
        playerStats.health = 100;
    }

    void Start()
    {

        hpController = gameObject.GetComponentInChildren<HPController>();
        playerStats = new PlayerStats(300, 300, 300, 2, 100);
        //Range = 2;
        //Speed = 100;
        //MaxHP = 300;
        //CurrentHP = MaxHP;
        //Damage = 300;
        //position = new Vector2(0, 0);
    }


    // Update is called once per frame
    void Update()
    {
        CurrentHP = playerStats.health;
        Damage = playerStats.attackDamage;
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

    public void setHP(bool IsAttack, int damage)
    {
        if(!IsAttack)
        {
            return;
        }
        playerStats.health = playerStats.health - damage;
        //hpController.updateHPText(CurrentHP);
        IsAttack = false;
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
