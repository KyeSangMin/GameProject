using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HPController : MonoBehaviour
{

    // private TextMeshPro textHp; //TextMeshPro ≈ÿΩ∫∆Æ
    [SerializeField]
    private TextMeshProUGUI tmp_text;
   // private CharacterStats characterStats;

    // Start is called before the first frame update
    void Start()
    {
        //characterStats = gameObject.GetComponentInParent<CharacterStats>();
        //textHp = gameObject.GetComponent<TextMeshPro>();
        //tmp_text = GetComponent<TextMeshProUGUI>;

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void updateHPText(int _HP)
    {
        tmp_text.text = _HP.ToString();
    }

    public void setText()
    {

    }

}
