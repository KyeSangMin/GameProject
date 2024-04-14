using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public sealed class BTActionNode : BTNode
{
    Func<BTNode.BTNodeState> _onUpdate = null;

    public BTActionNode(Func<BTNode.BTNodeState> onUpdate)
    {
        _onUpdate = onUpdate;
    }

    public override BTNode.BTNodeState Evaluate() => _onUpdate?.Invoke() ?? BTNode.BTNodeState.Failure;
}

