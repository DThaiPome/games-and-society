using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPuzzleNode : MonoBehaviour
{
    [SerializeField]
    public bool on { get; private set; }

    public SpriteRenderer renderer;
    public Sprite onSprite;
    public Sprite offSprite;

    Vector2 gridPos;
    private List<LightPuzzleNode> neighbors;

    void Awake()
    {
        this.on = false;
        this.neighbors = new List<LightPuzzleNode>();
    }

    void Start()
    {
        EventManager.instance.onClickEvent += changeThis;
    }

    void Update()
    {
        if (this.on)
        {
            renderer.sprite = this.onSprite;
        } else
        {
            renderer.sprite = this.offSprite;
        }
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
        this.changeEvents();
    }

    private void changeEvents()
    {
        this.getPuzzle().checkForCompletion(this);
        EventManager.instance.onLightPuzzleSwitch(this);
    }

    public bool checkForCompletion()
    {
        return this.puzzleComplete(new List<LightPuzzleNode>());
    }

    private bool puzzleComplete(List<LightPuzzleNode> alreadyChecked)
    {
        alreadyChecked.Add(this);
        foreach (LightPuzzleNode neighbor in neighbors)
        {
            if (!this.containsNode(alreadyChecked, neighbor) && !neighbor.puzzleComplete(alreadyChecked))
            {
                return false;
            }
        }
        return this.on;
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

    public Vector2 getGridPos()
    {
        return this.gridPos;
    }

    public bool sameNode(LightPuzzleNode lpn)
    {
        return this.gridPos.Equals(lpn.gridPos);
    }

    public LightPuzzle getPuzzle()
    {
        return this.transform.parent.GetComponent<LightPuzzle>();
    }
}