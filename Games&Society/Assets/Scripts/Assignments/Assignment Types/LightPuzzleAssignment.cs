using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPuzzleAssignment : Assignment
{
    private float spaceBetweenPuzzles;
    private List<LightPuzzle> puzzles;

    public LightPuzzleAssignment(Transform transform, int minuteStart, int minuteDue, float difficulty, float spaceBetweenPuzzles)
        : base(transform, minuteStart, minuteDue, difficulty)
    {
        this.spaceBetweenPuzzles = spaceBetweenPuzzles;
        this.puzzles = new List<LightPuzzle>();
        this.generate();
    }

    public override void generate()
    {
        int nodes = this.difficultyToNodeCount();
        int puzzleCount = this.difficultyToPuzzleCount();

        this.puzzles.Add(
            LightPuzzleGen.instance
            .newPuzzle(
                new Vector2(this.spaceBetweenPuzzles / 2, this.spaceBetweenPuzzles / 2),
                nodes,
                this.transform));
        this.puzzles.Add(
            LightPuzzleGen.instance
            .newPuzzle(
                new Vector2(-this.spaceBetweenPuzzles / 2, this.spaceBetweenPuzzles / 2),
                nodes,
                this.transform));

        if (puzzleCount == 3)
        {
            this.puzzles.Add(
            LightPuzzleGen.instance
            .newPuzzle(
                new Vector2(0, -this.spaceBetweenPuzzles / 2),
                nodes,
                this.transform));
        } else
        {
            this.puzzles.Add(
            LightPuzzleGen.instance
            .newPuzzle(
                new Vector2(this.spaceBetweenPuzzles / 2, -this.spaceBetweenPuzzles / 2),
                nodes,
                this.transform));
            this.puzzles.Add(
                LightPuzzleGen.instance
                .newPuzzle(
                    new Vector2(-this.spaceBetweenPuzzles / 2, -this.spaceBetweenPuzzles / 2),
                    nodes,
                    this.transform));
        }
    }

    public override float grade()
    {
        int totalNodes = 0;
        int onNodes = 0;
        foreach(LightPuzzle lp in this.puzzles)
        {
            totalNodes += lp.nodeCount;
            onNodes += lp.onNodeCount();
        }
        return (float)onNodes / (float)totalNodes;
    }

    public override string emailHeader()
    {
        //TODO: Make a "toTime" method in GameTime .. or something
        return "You have a new math assignment due at " + this.minuteDue;
    }

    private int difficultyToNodeCount()
    {
        int maxNodes = 10;
        float C = 0.9f;
        return (int)(maxNodes * Mathf.Sqrt(C * this.difficulty));
    }

    private int difficultyToPuzzleCount()
    {
        return this.difficulty >= 0.4 ? 4 : 1;
    }

    public override void show()
    {
        foreach(LightPuzzle lp in this.puzzles)
        {
            lp.gameObject.SetActive(true);
        } 
    }

    public override void hide()
    {
        foreach (LightPuzzle lp in this.puzzles)
        {
            lp.gameObject.SetActive(false);
        }
    }
}
