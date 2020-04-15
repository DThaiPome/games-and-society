using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPuzzle : MonoBehaviour
{
    [SerializeField]
    private float widthBetweenNodes;

    private List<LightPuzzleNode> nodes;

    void Awake()
    {
        this.nodes = new List<LightPuzzleNode>();
    }

    void Start()
    {
        EventManager.instance.onLightPuzzleSwitchEvent += checkForCompletion;
    }

    void Update()
    {
        foreach(LightPuzzleNode lpn in this.nodes)
        {
            lpn.setPos(this.widthBetweenNodes);
        }
    }
    
    public void setLocalPos(Vector2 pos)
    {
        this.transform.localPosition = pos;
    }

    //Checks if the game is won, and does something if it is
    private void checkForCompletion(bool newState)
    {
        LightPuzzleNode lpn = this.nodes[0];
        bool win = false;
        if (lpn != null)
        {
            win = lpn.checkForCompletion();
        }

        if (win)
        {
            Debug.Log("WIN (Put something here lol)");
        }
    }

    //Initializes a random puzzle with the given number of nodes
    public void initPuzzle(int nodeCount)
    {
        this.initNodes(nodeCount);
        this.setNeighbors();
        this.changeRandomly(Random.Range(1, nodeCount));
    }

    public void changeRandomly(int nodeCount)
    {
        List<LightPuzzleNode> unpickedNodes = new List<LightPuzzleNode>(this.nodes);
        while(nodeCount > 0)
        {
            int i = Random.Range(0, unpickedNodes.Count);
            LightPuzzleNode lpn = unpickedNodes[i];
            unpickedNodes.RemoveAt(i);
            lpn.change();
            nodeCount--;
        }
    }

    //Initializes the nodes in a list
    private void initNodes(int nodeCount)
    {
        nodeCount = nodeCount < 3 ? 3 : nodeCount;
        Queue<LightPuzzleNode> workList = new Queue<LightPuzzleNode>();
        workList.Enqueue(this.newNode(new Vector2(0, 0)));
        int nodesLeft = nodeCount - 1;
        while (nodesLeft > 0)
        {
            LightPuzzleNode current = workList.Dequeue();

            int maxNodes = nodesLeft < 4 ? nodesLeft : 4;
            int nodesToCreate = Random.Range(0, maxNodes) + 1;
            nodesLeft -= nodesToCreate - this.createRandomNodesAround(current, nodesToCreate, workList);
        }
    }

    private void setNeighbors()
    {
        foreach(LightPuzzleNode lpn in this.nodes)
        {
            this.makeNeighborsIfExists(lpn, lpn.getGridPos() + new Vector2(1, 0));
            this.makeNeighborsIfExists(lpn, lpn.getGridPos() + new Vector2(0, 1));
            this.makeNeighborsIfExists(lpn, lpn.getGridPos() + new Vector2(-1, 0));
            this.makeNeighborsIfExists(lpn, lpn.getGridPos() + new Vector2(0, -1));
        }
    }

    private void makeNeighborsIfExists(LightPuzzleNode lpn, Vector2 atPos)
    {
        if (this.nodeExistsAt(atPos))
        {
            lpn.makeNeighborsWith(this.getNodeAt(atPos));
        }
    }

    //Creates a given number of nodes around the given node, without overlapping existing nodes.
    //Queues any created nodes to the given queue and returns the number of nodes not created.
    private int createRandomNodesAround(LightPuzzleNode lpn, int count, Queue<LightPuzzleNode> q)
    {
        Vector2 gridPos = lpn.getGridPos();
        List<Vector2> unpickedGridPos = new List<Vector2>
        {
            new Vector2(1, 0) + gridPos,
            new Vector2(0, 1) + gridPos,
            new Vector2(-1, 0) + gridPos,
            new Vector2(0, -1) + gridPos,
        };

        while (count > 0 && unpickedGridPos.Count > 0)
        {
            int i = Random.Range(0, unpickedGridPos.Count);
            Vector2 gp = unpickedGridPos[i];
            unpickedGridPos.RemoveAt(i);

            if (!this.nodeExistsAt(gp))
            {
                count--;
                LightPuzzleNode node = this.newNode(gp);
                q.Enqueue(node);
            }
        }

        return count;
    }

    //Checks if a node exists at the given grid position
    private bool nodeExistsAt(Vector2 gridPos)
    {
        foreach(LightPuzzleNode lpn in this.nodes)
        {
            if (lpn.getGridPos().Equals(gridPos))
            {
                return true;
            }
        }
        return false;
    }

    private LightPuzzleNode getNodeAt(Vector2 gridPos)
    {
        foreach(LightPuzzleNode lpn in this.nodes)
        {
            if (lpn.getGridPos().Equals(gridPos))
            {
                return lpn;
            }
        }
        return null;
    }

    private LightPuzzleNode newNode(Vector2 gridPos)
    {
        LightPuzzleNode lpn = LightPuzzleGen.instance.newNode(gridPos);
        this.nodes.Add(lpn);
        lpn.transform.SetParent(this.transform);
        return lpn;
    }
}
