using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public int HelloWorld = 2;

    private GameObject currentObject;
  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if (hit.collider.CompareTag("GridTile"))
            {
                currentObject.GetComponent<characterAction>().setMoveGrid(hit.collider.gameObject.GetComponent<GridTile>().getTIle());
                currentObject = null;

            }



        }
    }

    public void setCurrentObject(GameObject Ally)
    {
        currentObject = Ally;
    }

    public GameObject getCurrentObject()
    {
        return currentObject;
    }






}
