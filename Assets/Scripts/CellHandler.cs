using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellHandler : MonoBehaviour
{
    public Sprite spriteO;
    public Sprite spriteX;
    public CellPosition cellPosition;
    private Button button;
    private Image image;
    private CellState _currentState = CellState.NONE;
    public BoardHandler boardHandler { get; set; }
    public CellState currentState
    {
        get => _currentState;
        set
        {
            _currentState = value;
            renderByState(value);
        }
    }

    public void Start()
    {
        button = GetComponent<Button>();
        image = button.GetComponent<Image>();
    }
    public void onButtonClick()
    {
        boardHandler.onCellClicked(_currentState, cellPosition);
    }
    private void renderByState(CellState state)
    {
        switch (state)
        {
            case CellState.O:
                image.sprite = spriteO;
                break;
            case CellState.X:
                image.sprite = spriteX;
                break;
            case CellState.NONE:
            default:
                image.sprite = null;
                break;
        }
    }
}
