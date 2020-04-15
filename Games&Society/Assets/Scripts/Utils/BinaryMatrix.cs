using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryMatrix
{
    private int[][] matrix;
    private int rows;
    private int columns;

    public BinaryMatrix(int rows, int columns)
    {
        this.rows = rows;
        this.columns = columns;

        this.matrix = new int[this.rows][];
        for (int i = 0; i < this.rows; i++)
        {
            this.matrix[i] = new int[this.columns];
        }
    }

    public void set(int val, int row, int column)
    {
        this.matrix[row][column] = val;
    }

    public int zeroRows()
    {
        int total = 0;
        for (int i = 0; i < this.rows; i++)
        {
            bool allZeroes = true;
            for (int j = 0; j < this.columns && allZeroes; j++)
            {
                if (this.matrix[i][j] == 1)
                {
                    allZeroes = false;
                }
            }
            if (allZeroes)
            {
                total++;
            }
        }
        return total;
    }

    public void rowEchelonForm()
    {
        int currentColumn = 0;
        int currentRow = 0;
        while (currentColumn < this.columns)
        {
            int pivotColumn = this.leftmostNonZeroColumn(currentColumn);
            if (currentColumn >= this.columns)
                break;

            this.bring1ToRow(pivotColumn, currentRow);
            this.allZeroesBelowRow(pivotColumn, currentRow);

            currentColumn = pivotColumn + 1;
            currentRow++;
        }
    }

    private int leftmostNonZeroColumn(int startColumn)
    {
        int i;
        for (i = startColumn; i < this.columns && !this.nonZeroColumn(i); i++) { }
        return i;
    }

    private bool nonZeroColumn(int column)
    {
        for(int i = 0; i < this.rows; i++)
        {
            if (this.matrix[i][column] == 1)
            {
                return true;
            }
        }
        return false;
    }

    private void bring1ToRow(int row, int column)
    {
        for (int i = row + 1; i < this.rows; i++)
        {
            if (this.matrix[i][column] == 1)
            {
                this.swapRows(row, i);
                return;
            }
        }
    }

    private void allZeroesBelowRow(int row, int column)
    {
        for (int i = row + 1; i < this.rows; i++)
        {
            if (this.matrix[i][column] == 1)
            {
                this.subtractRows(i, row);
            }
        }
    }

    private void subtractRows(int to, int from)
    {
        for(int i = 0; i < this.columns; i++)
        {
            this.matrix[to][i] = this.binarySubtract(this.matrix[to][i], this.matrix[from][i]);
        }
    }
    private int binarySubtract(int i1, int i2)
    {
        if (i1 == i2)
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }

    private void swapRows(int r1, int r2)
    {
        for(int i = 0; i < this.columns; i++)
        {
            int t = this.matrix[r1][i];
            this.matrix[r1][i] = this.matrix[r2][i];
            this.matrix[r2][i] = t;
        }
    }
}
