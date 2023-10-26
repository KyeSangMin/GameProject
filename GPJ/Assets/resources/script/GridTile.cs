using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour
{
    private int tileState;




    // Start is called before the first frame update
    void Start()
    {
        tileState = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseDown()
    {
        getTIle();
        Debug.Log(getTIle());
    }

    public GameObject getTIle()
    {
        return this.gameObject;
    }

}
