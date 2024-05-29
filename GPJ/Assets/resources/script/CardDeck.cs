using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour
{
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCardDeck();
    }

    public void MoveCardDeck()
    {
        Vector3 target;
        if (!gameManager.isCard)
        {
            target = new Vector3(transform.position.x, -3f, transform.position.z);
            this.transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * 10);

        }
        else if (gameManager.isCard)
        {
            target = new Vector3(transform.position.x, -7f, transform.position.z);
            this.transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * 10);
        }


    }
}
