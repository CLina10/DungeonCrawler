using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] bool displayRecursivelyFoundLeaves;
    [SerializeField] KeyCode kc_GrowBSP = KeyCode.Space;
    [SerializeField] KeyCode kc_GenerateRooms = KeyCode.Return;
    BSPTree bspTree;
    List<Rect> rooms = new List<Rect>(32);
    List<BSPNode> alternativeLeaves = new List<BSPNode>(32);
    bool vis = false;

    void Start()
    {
        bspTree = new BSPTree();
        vis = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(kc_GrowBSP))
        {
            bspTree.Grow();
            if (displayRecursivelyFoundLeaves)
            {
                alternativeLeaves.Clear();
                bspTree.FillLeavesList(bspTree.Root, alternativeLeaves);
            }
        }
        if (Input.GetKeyDown(kc_GenerateRooms))
        {
            MakeRooms();
        }
    }

    void MakeRooms()
    {
        rooms.Clear();
        for (int i = 0; i < bspTree.Leaves.Count; i++)
        {
            var leafRect = bspTree.Leaves[i].Rect;
            float newWidth = leafRect.width * Random.Range(0.4f, 0.8f);
            float newHeight = leafRect.height * Random.Range(0.4f, 0.8f);
            float xMargin = leafRect.width - newWidth;
            float yMargin = leafRect.height - newHeight;
            xMargin *= Random.Range(0.2f, 0.8f);
            yMargin *= Random.Range(0.2f, 0.8f);
            Rect room = new Rect(leafRect.x + xMargin, leafRect.y + yMargin, newWidth, newHeight);
            rooms.Add(room);
        }
    }

    // momentan rechnen wir die Darstellung in der Größe herunter! (* 0.1f) (nur zum testen)

    private void OnDrawGizmos()
    {
        if (!vis)
        {
            return;
        }
        Gizmos.color = Color.white;
        var leaves = displayRecursivelyFoundLeaves ? alternativeLeaves : bspTree.Leaves;
        foreach (BSPNode node in leaves)
        {
            Gizmos.DrawWireCube(new Vector3(node.Rect.x + (node.Rect.width * 0.5f), 0f, node.Rect.y + (node.Rect.height * 0.5f)) * 0.1f, new Vector3(node.Rect.width, 0f, node.Rect.height) * 0.1f);
        }
        Gizmos.color = Color.cyan;
        foreach (Rect room in rooms)
        {
            Gizmos.DrawWireCube(new Vector3(room.x + (room.width * 0.5f), 0f, room.y + (room.height * 0.5f)) * 0.1f, new Vector3(room.width, 0f, room.height) * 0.1f);
        }
    }
}
