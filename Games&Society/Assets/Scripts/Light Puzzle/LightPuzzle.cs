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

    //Initializes a random puzzle with the given number of nodes
    public void initPuzzle(int nodeCount)
    {
        this.initNodes(nodeCount);
        int matchesNeeded = this.matches() - this.requiredMatches();
        this.makeRandomMatches(matchesNeeded);
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

        this.setNeighbors();
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
                Debug.Log(gp);
                count--;
                LightPuzzleNode node = this.newNode(gp);
                if (Random.value < 0.5)
                {
                    node.change();
                }
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

    private int requiredMatches()
    {
        BinaryMatrix matrix = this.puzzleToMatrix();

        matrix.rowEchelonForm();
        return matrix.zeroRows();
    }

    private BinaryMatrix puzzleToMatrix()
    {
        BinaryMatrix matrix = new BinaryMatrix(this.nodes.Count, this.nodes.Count);
        for (int i = 0; i < this.nodes.Count; i++)
        {
            for (int j = 0; j < this.nodes.Count; j++)
            {
                if (this.nodes[i].neighborsWith(this.nodes[j]))
                {
                    matrix.set(1, j, i);
                }
            }
        }

        return matrix;
    }

    private int matches()
    {
        int count = 0;
        for(int i = 0; i < this.nodes.Count; i++)
        {
            count += this.nodes[i].matches();
        }

        return count / 2;
    }

    private void makeRandomMatches(int matches)
    {
        List<LightPuzzleNode> unpickedNodes = new List<LightPuzzleNode>(this.nodes);
        while(matches > 0 && unpickedNodes.Count > 0)
        {
            int i = Random.Range(0, unpickedNodes.Count);
            if (unpickedNodes[i].matchWithRandomNeighbor())
            {
                matches--;
            } else
            {
                unpickedNodes.RemoveAt(i);
            }
        }
    }

    private LightPuzzleNode newNode(Vector2 gridPos)
    {
        LightPuzzleNode lpn = LightPuzzleGen.instance.newNode(gridPos);
        this.nodes.Add(lpn);
        lpn.transform.SetParent(this.transform);
        return lpn;
    }
}
