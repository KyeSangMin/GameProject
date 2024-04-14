using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BTNode 
{
    public enum BTNodeState
    {
        Running,
        Success,
        Failure
    }

    public abstract  BTNodeState Evaluate();


}
