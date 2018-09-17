using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSPTree
{
    public BSPNode Root { get; private set; }
    public List<BSPNode> Leaves { get; private set; } = new List<BSPNode>(32);

    public BSPTree()
    {
        Root = new BSPNode(null, new Rect(0f, 0f, 400, 300), false);
        Leaves.Add(Root);
        Debug.Log(Leaves.Count);
    }

    public void Grow()
    {
        List<BSPNode> newLeaves = new List<BSPNode>(32); for (int i = 0; i < Leaves.Count; i++)
        {
            float percent = Random.Range(0.3f, 0.7f); bool vertical;
            // Chance besteht, daß die gleiche Richtung gewählt wird
            vertical = Random.value < 0.05f ? Leaves[i].SplitVertical : !Leaves[i].SplitVertical;
            //vertical = !leaves[i].splitVertical;// so wechselt stattdessen jedesmal die Richtung
            newLeaves.AddRange(Leaves[i].SplitNode(percent, vertical));
        }
        Leaves = newLeaves;
    }

    public void FillLeavesList(BSPNode node, List<BSPNode> leaves)
    {
        if (node.Children[0] != null)
        {
            for (int i = 0; i < 2; i++)
            {
                FillLeavesList(node.Children[i], leaves);
            }
        }
        else
        {
            leaves.Add(node);
        }
    }
}
