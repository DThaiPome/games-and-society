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
                nodes + 1,
                this.transform));
        } else
        {
            this.puzzles.Add(
            LightPuzzleGen.instance
            .newPuzzle(
                new Vector2(this.spaceBetweenPuzzles / 2, -this.spaceBetweenPuzzles / 2),
                nodes + 1,
                this.transform));
            this.puzzles.Add(
                LightPuzzleGen.instance
                .newPuzzle(
                    new Vector2(-this.spaceBetweenPuzzles / 2, -this.spaceBetweenPuzzles / 2),
                    nodes + 1,
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
        Debug.Log(onNodes + " / " + totalNodes);
        return (float)onNodes / (float)totalNodes;
    }

    public override string emailHeader()
    {
        int hour = Utils.minutesToMilitaryHour(this.minuteDue);
        int minute = Utils.minutesToHourlyMinute(this.minuteDue);
        string hourText = hour < 10 ? "0" + hour : "" + hour;
        string minuteText = minute < 10 ? "0" + minute : "" + minute;
        return "You have a new math assignment (" + this.assignmentCode() + ") due at " + hourText + ":" + minuteText;
    }

    public override string assignmentCode()
    {
        string idText = "" + (this.id % 1000);
        if (this.id < 100)
        {
            idText = "0" + idText;
            if (this.id < 10)
            {
                idText = "0" + idText;
            }
        }
        return "MAT" + idText;
    }

    private int difficultyToNodeCount()
    {
        int maxNodes = 10;
        float C = 0.9f;
        return Mathf.Clamp((int)(maxNodes * Mathf.Sqrt(C * this.difficulty)), 3, 24);
    }

    private int difficultyToPuzzleCount()
    {
        return this.difficulty >= 0.4 ? 4 : 3;
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

    public override void destroy()
    {
        foreach (LightPuzzle lp in this.puzzles)
        {
            lp.destroy();
        }
    }
}
