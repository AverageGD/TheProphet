using BehaviorTree;
using System.Collections.Generic;
using UnityEngine;

public class PapaEmeritBTree : BTree
{
    [SerializeField] private Transform _transform;

    protected override Node SetupTree()
    {
        Node root = new Sequence(new List<Node>
        {
            new LookAtPlayer(_transform)
        }) ;

        return root;
    }
}
