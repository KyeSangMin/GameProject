using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class SelectorNode : BTNode
{
    List<BTNode> _childs;

    public SelectorNode(List<BTNode> childs)
    {
        _childs = childs;
    }

    public override  BTNode.BTNodeState Evaluate()
    {
        if (_childs == null)
            return BTNode.BTNodeState.Failure;

        foreach (var child in _childs)
        {
            switch (child.Evaluate())
            {
                case BTNode.BTNodeState.Running:
                    return BTNode.BTNodeState.Running;
                case BTNode.BTNodeState.Success:
                    return BTNode.BTNodeState.Success;
            }
        }

        return BTNode.BTNodeState.Failure;
    }
}