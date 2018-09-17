using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Binary Space Partitioning - BSP Node
public class BSPNode
{
    public BSPNode Parent { get; private set; }
    public BSPNode[] Children { get; private set; } = new BSPNode[2];
    public BSPNode Sibling { get; set; }// momentan unbenutzt
    public Rect Rect { get; private set; }
    public bool SplitVertical { get; private set; }

    public BSPNode(BSPNode parent, Rect rect, bool vertical)
    {
        Parent = parent;
        Rect = rect;
        SplitVertical = vertical;
    }

    public BSPNode[] SplitNode(float percent, bool vertical)
    {
        for (int i = 0; i < Children.Length; i++)
        {
            Rect newRect;
            if (i == 0)// erstes child
            {
                if (!vertical)
                {
                    newRect = new Rect(Rect.x, Rect.y, Rect.width, Rect.height * percent);
                }
                else
                {
                    newRect = new Rect(Rect.x, Rect.y, Rect.width * percent, Rect.height);
                }
            }
            else// zweites child
            {
                if (!vertical)
                {
                    newRect = new Rect(Rect.x, Rect.y + Rect.height * percent, Rect.width, Rect.height * (1 - percent));
                }
                else
                {
                    newRect = new Rect(Rect.x + Rect.width * percent, Rect.y, Rect.width * (1 - percent), Rect.height);
                }
            }
            Children[i] = new BSPNode(this, newRect, vertical);
        }
        return Children;
    }
}
