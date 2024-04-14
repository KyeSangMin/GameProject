using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class SequenceNode : BTNode
{
    List<BTNode> _childs;

    public SequenceNode(List<BTNode> childs)
    {
        _childs = childs;
    }

    public override BTNode.BTNodeState Evaluate()
    {
        if (_childs == null || _childs.Count == 0)
            return BTNode.BTNodeState.Failure;

        foreach (var child in _childs)
        {
            switch (child.Evaluate())
            {
                case BTNode.BTNodeState.Running:
                    return BTNode.BTNodeState.Running;

                case BTNode.BTNodeState.Success:
                    continue;

                case BTNode.BTNodeState.Failure:
                    return BTNode.BTNodeState.Failure;
            }
        }

        return BTNode.BTNodeState.Success;
    }
}