using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardHandler : MonoBehaviour
{
    const int SIZE = 3;
    public Button[] cells;
    public Text winText;
    private PlayerState playerState = PlayerState.O;

    private bool _didSomeoneWin = false;
    public bool didSomeoneWin
    {
        get => _didSomeoneWin;
        set
        {
            _didSomeoneWin = value;
            winText.gameObject.SetActive(_didSomeoneWin);
        }
    }

    void Start()
    {
        winText.gameObject.SetActive(false);

        for (int i = 0; i < SIZE; i++)
        {
            for (int j = 0; j < SIZE; j++)
            {
                int index = indexBy(j, i);
                var cellHandler = cells[index].GetComponent<CellHandler>();
                cellHandler.cellPosition = new CellPosition(j, i);
                cellHandler.boardHandler = this;
            }
        }
    }

    public void onCellClicked(CellState state, CellPosition cellPosition)
    {
        switch (state)
        {
            case CellState.NONE:
                if (!didSomeoneWin)
                {
                    playTurn(state, cellPosition);

                    if (checkWin())
                    {
                        didSomeoneWin = true;
                    }

                }
                break;
            case CellState.X:
            case CellState.O:
                break;
        }
    }

    public void resetGame()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            var cellHandler = cells[i].GetComponent<CellHandler>();
            cellHandler.currentState = CellState.NONE;
        }
        didSomeoneWin = false;
    }

    void playTurn(CellState cellState, CellPosition cellPosition)
    {
        int index = indexBy(cellPosition);
        var cellHandler = cells[index].GetComponent<CellHandler>();
        switch (playerState)
        {
            case PlayerState.O:
                cellHandler.currentState = CellState.O;
                playerState = PlayerState.X;
                break;

            case PlayerState.X:
                cellHandler.currentState = CellState.X;
                playerState = PlayerState.O;
                break;
        }
    }

    bool checkWin()
    {
        if (checkHorizontalLine() || checkVerticalLine() || checkDiagonalLines())
        {
            return true;
        }
        return false;
    }
    CellState GetState(Button button)
    {
        return button.GetComponent<CellHandler>().currentState;
    }
    bool checkHorizontalLine()
    {
        for (int i = 0; i < SIZE; i++)
        {
            if (areTheseEqual((i, 0), (i, 1), (i, 2)))
            {
                return true;
            }
        }

        return false;
    }
    bool checkVerticalLine()
    { //0i 1i 2i
        for (int i = 0; i < SIZE; i++)
        {
            if (areTheseEqual((0, i), (1, i), (2, i)))
            {
                return true;
            }
        }

        return false;
    }

    bool checkDiagonalLines()
    {
        if (areTheseEqual((0, 0), (1, 1), (2, 2)) || areTheseEqual((0, 2), (1, 1), (2, 0)))
        {
            return true;
        }

        return false;
    }
    bool areTheseEqual((int x, int y) first, (int x, int y) second, (int x, int y) third)
    {
        var firstState = GetState(cells[indexBy(first.x, first.y)]);
        var secondState = GetState(cells[indexBy(second.x, second.y)]);
        var thirdState = GetState(cells[indexBy(third.x, third.y)]);

        if (firstState == CellState.NONE || secondState == CellState.NONE || thirdState == CellState.NONE)
        {
            return false;
        }

        if ((firstState == secondState) && (firstState == thirdState))
        {
            return true;
        }

        return false;
    }
    int indexBy(CellPosition cellPosition)
    {
        return indexBy(cellPosition.x, cellPosition.y);
    }
    int indexBy(int x, int y)
    {
        return (SIZE * y) + x;
    }
}
