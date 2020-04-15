using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPuzzleNode : MonoBehaviour
{
    [SerializeField]
    private bool on;

    public Animator anim;

    Vector2 gridPos;
    private List<LightPuzzleNode> neighbors;

    void Awake()
    {
        this.on = false;
        this.neighbors = new List<LightPuzzleNode>();
    }

    void Start()
    {
        EventManager.instance.registerToClickEvent(changeThis);
    }

    void Update()
    {
        this.anim.SetBool("on", this.on);
    }

    private void changeThis(Transform t)
    {
        if (t.Equals(this.transform))
        {
            this.change();
        }
    }

    //Sets the position of this node in the puzzle grid
    public void setGridPos(Vector2 gridPos)
    {
        this.gridPos = gridPos;
    }

    //Uses the grid position to place this object in the scene
    public void setPos(float mag)
    {
        this.transform.localPosition = this.gridPos * mag;
    }

    //Inverts the state of this node and the nodes immediately around it
    public void change()
    {
        this.on = !this.on;
        foreach(LightPuzzleNode lpn in this.neighbors)
        {
            lpn.on = !lpn.on;
        }
        //TODO: also get rid of this and put it somewhere else. Maybe like an event or something
        this.checkForCompletion();
    }

    private void checkForCompletion()
    {
        this.puzzleComplete(new List<LightPuzzleNode>());
    }

    private bool puzzleComplete(List<LightPuzzleNode> alreadyChecked)
    {
        if (!this.on)
        {
            return false;
        }
        else
        {
            alreadyChecked.Add(this);
            foreach (LightPuzzleNode neighbor in neighbors)
            {
                if (this.containsNode(alreadyChecked, neighbor))
                {
                    break;
                }   
                if (!neighbor.puzzleComplete(alreadyChecked))
                {
                    return false;
                }
            }
            return true;
        }
    }

    private bool containsNode(List<LightPuzzleNode> list, LightPuzzleNode lpn)
    {
        foreach (LightPuzzleNode other in list)
        {
            if (lpn.sameNode(other))
            {
                return true;
            }
        }
        return false;
    }

    //Matches states with a random neighbor. Returns false if this node is matched with all neighbors,
    //and true otherwise.
    public bool matchWithRandomNeighbor()
    {
        List<LightPuzzleNode> unpickedNodes = new List<LightPuzzleNode>(this.neighbors);
        while(unpickedNodes.Count > 0)
        {
            int i = Random.Range(0, unpickedNodes.Count);
            LightPuzzleNode lpn = unpickedNodes[i];
            unpickedNodes.RemoveAt(i);
            if (this.matchWith(lpn))
            {
                return true;
            }
        }
        return false;
    }

    //Matches states with the given LPN. Returns false if this node is already matched, and
    //true otherwise.
    private bool matchWith(LightPuzzleNode lpn)
    {
        if (this.on != lpn.on)
        {
            this.on = lpn.on;
            return true;
        }
        return false;
    }

    //Returns the count of neighbors around this node
    public int neighborCount()
    {
        return this.neighbors.Count;
    }
    
    public bool neighborsWith(LightPuzzleNode lpn)
    {
        foreach(LightPuzzleNode neighbor in this.neighbors)
        {
            if (lpn.sameNode(neighbor))
            {
                return true;
            }
        }

        return this.sameNode(lpn);
    }

    //Adds the given lpn as a neighbor to this one ONLY
    public void makeNeighborsWith(LightPuzzleNode lpn)
    {
        this.neighbors.Add(lpn);
    }

    public int matches()
    {
        int count = 0;
        foreach(LightPuzzleNode lpn in this.neighbors)
        {
            if (this.on == lpn.on)
            {
                count++;
            }
        }
        return count;
    }

    public Vector2 getGridPos()
    {
        return this.gridPos;
    }

    public bool sameNode(LightPuzzleNode lpn)
    {
        return this.gridPos.Equals(lpn.gridPos);
    }
}