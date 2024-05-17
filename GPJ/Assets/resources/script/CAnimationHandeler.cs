using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAnimationHandeler : MonoBehaviour
{

    Animator animator;
    BattleSystem battleSystem;
    BattleGrid battleGrid;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
        battleSystem = GameObject.Find("BattleSystem").GetComponent<BattleSystem>(); 
        battleGrid = GameObject.Find("BattleGrid").GetComponent<BattleGrid>();
    }
    // Update is called once per frame
    private void AttackEnd()
    {
        animator.SetBool("isAttack", false);
        this.gameObject.GetComponentInParent<characterAction>().setAtackObject(null);
    }

    private void DieEnd()
    {
        GameObject parent = this.gameObject.GetComponentInParent<characterAction>().gameObject;
        battleGrid.FindGridTIle((int)parent.GetComponent<CharacterStats>().getCharacterPos().x, (int)parent.GetComponent<CharacterStats>().getCharacterPos().y).GetComponent<GridTile>().ChangeState(GridTile.TileState.None);
        //battleGrid.resetTileState();
        battleSystem.DestroyCharacter(parent);
        Destroy(this.gameObject.transform.parent.gameObject);
        //battleSystem.updateIncreaseSpeed();
    }
    

}
