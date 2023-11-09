using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAnimationHandeler : MonoBehaviour
{

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();

    }
    // Update is called once per frame
    private void AttackEnd()
    {
        Debug.Log("AttackEnd");
        animator.SetBool("isAttack", false);
        this.gameObject.GetComponentInParent<characterAction>().setAtackObject(null);
    }
}
