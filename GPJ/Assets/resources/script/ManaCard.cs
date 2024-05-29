using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaCard : MonoBehaviour
{
    public bool selectCard;
    public int sortNum;
    public float cardPos;
    private GameObject deleteZone; // 삭제 영역
    public int EventNum;
    BattleSystem battleSysyem;
    EffectManager effectManager;
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        selectCard = false;
        deleteZone = GameObject.FindGameObjectWithTag("DeleteZone"); // 삭제 영역 찾기
        battleSysyem = GameObject.Find("BattleSystem").GetComponent<BattleSystem>();
        effectManager = EffectManager.Instance;
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (selectCard)
        {
            UpdateCardpos();
        }
        MoveCard();
        /*
       Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       LayerMask cardLayerMask = LayerMask.GetMask("Card");
       RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, Vector2.zero, Mathf.Infinity, cardLayerMask);
       RaycastHit2D hits2 = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, cardLayerMask);


       GameObject currentHoveredObject = null;

       foreach (RaycastHit2D hit in hits)
       {
           if (hit.collider != null)
           {
               currentHoveredObject = hit.collider.gameObject;
               break;
           }
       }
      
        if (hits2.collider != null)
        {
            currentHoveredObject = hits2.collider.gameObject;
        }

  
        if (currentHoveredObject != lastHoveredObject)
        {
            if (lastHoveredObject != null)
            {
                OnMouseExitInternal(lastHoveredObject);
            }

            if (currentHoveredObject != null)
            {
                OnMouseEnterInternal(currentHoveredObject);
            }

            lastHoveredObject = currentHoveredObject;
        }
        */
    }
    /*
    private void OnMouseEnterInternal(GameObject obj)
    {
        var handler = obj.GetComponent<ManaCard>();
        if (handler != null && !handler.isMouseOver)
        {
            Debug.Log("OnEnter");
            obj.GetComponent<SpriteRenderer>().sortingOrder = this.sortNum + 4;
            obj.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = this.sortNum + 5;
            handler.isMouseOver = true;
        }
    }

    private void OnMouseExitInternal(GameObject obj)
    {
        var handler = obj.GetComponent<ManaCard>();

        if (handler != null)
        {
            handler.isMouseOver = false;
            obj.GetComponent<SpriteRenderer>().sortingOrder = this.sortNum;
            obj.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = this.sortNum+1;
            
        }
    }
    */
    private void OnMouseEnter()
    {
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = this.sortNum + 4;
        gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = this.sortNum + 5;
    }
    private void OnMouseExit()
    {
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = this.sortNum;
        gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = this.sortNum+1;
    }

    private void OnMouseDown()
    {
        selectCard = true;
    }

    private void OnMouseDrag()
    {
        selectCard = true;
    }

    private void OnMouseUp()
    {
        selectCard = false;

        // 카드가 삭제 영역에 있는지 확인
        if (IsCardInDeleteZone())
        {
            CardEvent();
            Destroy(gameObject); // 카드 삭제
        }
        else
        {
            // 원래 위치로 되돌리기 (필요에 따라 수정 가능)
            gameObject.transform.localPosition = new Vector3(cardPos, 0, 0);
        }
    }

    private void UpdateCardpos()
    {
        gameObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                Input.mousePosition.y, -Camera.main.transform.position.z));
    }
    private bool IsCardInDeleteZone()
    {
        if (deleteZone == null)
            return false;

        Collider2D deleteZoneCollider = deleteZone.GetComponent<Collider2D>();
        Collider2D cardCollider = GetComponent<Collider2D>();

        if (deleteZoneCollider == null || cardCollider == null)
            return false;

        return deleteZoneCollider.bounds.Intersects(cardCollider.bounds);
    }


    public void MoveCard()
    {
        Vector3 target;
        if (!gameManager.isCard)
        {
            target = new Vector3(transform.position.x, -7f, transform.position.z);
            this.transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * 10);

        }
        else if (gameManager.isCard)
        {
            target = new Vector3(transform.position.x, -3f, transform.position.z);
            this.transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * 10);
        }
        

    }
    public void CardEvent()
    {
        switch(EventNum)
        {
            case 1: // heal
                List<GameObject> allyList =  battleSysyem.allyCharList;
                foreach (GameObject ally in allyList)
                {
                    ally.GetComponent<CharacterStats>().EventSetHP(1, 100);
                    effectManager.StartEffectAni(1, ally.transform.position);
                }
                break;

            case 2: // damage
                List<GameObject> enemyList = battleSysyem.enemyCharList;
                foreach (GameObject enemy in enemyList)
                {
                    enemy.GetComponent<CharacterStats>().EventSetHP(2, 100);
                    effectManager.StartEffectAni(2, enemy.transform.position);
                }
                break;

            case 3: // defence
                List<GameObject> enemyList2 = battleSysyem.enemyCharList; ;
                foreach (GameObject enemy in enemyList2)
                {
                    enemy.GetComponent<CharacterStats>().EventSetDefence();
                    effectManager.StartEffectAni(3, enemy.transform.position);
                }
                break;
            
        }
    }
}
