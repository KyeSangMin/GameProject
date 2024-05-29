using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Health : MonoBehaviour
{

    public float maxHealth;
    public float currentHealth;
    public float maxSpeed = 10000f;
    public float currentSpeed;
    public Slider healthBar;
    public Slider SpeedBar;

    public CharacterStats characterStats;

    // Start is called before the first frame update
    void Start()
    {
        characterStats = this.gameObject.GetComponent<CharacterStats>();
        maxHealth = characterStats.getMaxHP();
        currentHealth = characterStats.getCurrentHP();
        currentSpeed = 0;

        // �����̴� ���� �ʱ�ȭ
        if (healthBar != null)
        {
            healthBar.minValue = 0;
            healthBar.maxValue = 1;
            healthBar.value = 1; // ���� �� ü�� �ٰ� �� ���ֵ��� ����
        }
        if (SpeedBar != null)
        {
            SpeedBar.minValue = 0;
            SpeedBar.maxValue = 1;
            SpeedBar.value = 1; // ���� �� ü�� �ٰ� �� ���ֵ��� ����
        }
        UpdateHealthBar();
        UpdateSpeedBar();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthBar();
        UpdateSpeedBar();

        if (healthBar != null)
        {
            // ������Ʈ�� ��ġ�� ���� ü�¹� ��ġ ������Ʈ
            healthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up* 0.5f +  Vector3.right * 0.1f);
        }
        if (SpeedBar != null)
        {
            // ������Ʈ�� ��ġ�� ���� ü�¹� ��ġ ������Ʈ
            SpeedBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 0.42f + Vector3.right * 0.1f);
        }
      


    }

    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            currentHealth = characterStats.getCurrentHP();
            healthBar.value = currentHealth / maxHealth;
        }
    }

    void UpdateSpeedBar()
    {
        if (SpeedBar != null)
        {
            SpeedBar.value = currentSpeed / 10000;
        }
    }


    public void UpdateSpeedGauge(int value)
    {
        currentSpeed = value;
    }

    public void OnSlideUI()
    {
        healthBar.GetComponent<GameObject>().SetActive(true);
        SpeedBar.GetComponent<GameObject>().SetActive(true);

    }

    public void OffSlidUI()
    {
        healthBar.GetComponent<GameObject>().SetActive(false);
        SpeedBar.GetComponent<GameObject>().SetActive(false);
    }
}
