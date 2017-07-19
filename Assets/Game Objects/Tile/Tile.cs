﻿using UnityEngine;
using System.Collections;

public enum TileState
{
	DEFAULT = 0,
	POSSIBLE, // Subdued version of piece color.
	DRAWN,    // Full piece color.
	KNIGHT_TRAVERSABLE,   // Darker grey, showing traversable space
	KNIGHT_TRAVERSED      // Full Knight color, down sprite
}

public class Tile : MonoBehaviour 
{
	[SerializeField] private Sprite downSprite;
	[SerializeField] private Sprite upSprite;

	[SerializeField] private Color defaultColor = Color.white;
	[SerializeField] private Color knightTraversableColor = Color.grey;

	[SerializeField] private TileState state = TileState.DEFAULT;

	private SpriteRenderer spriteRenderer;

	private Game parentGame;
	private IntVector2 currentCoordinates;

	void Awake()
	{
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		SetColorDefault();
	}

	public TileState GetState() 
	{
		return state;
	}

	public void SetState(TileState newState, Color color)
	{
		state = newState;

		switch(newState)
		{
			case TileState.DEFAULT:
				SetColorDefault();
				break;
			case TileState.POSSIBLE:
				SetColorPossible(color);
				break;
			case TileState.DRAWN:
				SetColorDrawn(color);
				break;
			case TileState.KNIGHT_TRAVERSABLE:
				SetColorKnightTraversable();
				break;
			case TileState.KNIGHT_TRAVERSED:
				SetColorKnightTraversed(color);
				break;
		}
	}

	public void SetColorDefault()
	{
		spriteRenderer.color = defaultColor;
		spriteRenderer.sortingOrder = 0;
		spriteRenderer.sprite = downSprite;
	}

	public void SetColorPossible(Color color) // takes in a subdued piece color
	{
		spriteRenderer.color = color;
		spriteRenderer.sortingOrder = 0;
		spriteRenderer.sprite = downSprite;
	}

	public void SetColorDrawn(Color color) // takes in a full piece color
	{
		spriteRenderer.color = color;
		spriteRenderer.sortingOrder = 1;
		spriteRenderer.sprite = upSprite;
	}

	public void SetColorKnightTraversable()
	{
		spriteRenderer.color = knightTraversableColor;
		spriteRenderer.sortingOrder = 0;
		spriteRenderer.sprite = downSprite;
	}

	public void SetColorKnightTraversed(Color color)
	{
		spriteRenderer.color = color;
		spriteRenderer.sortingOrder = 0;
		spriteRenderer.sprite = downSprite;
	}

	void OnEnable()
	{
		GameManager.Instance.GrowMe(this.gameObject, Random.Range(0f, 0.5f));
	}

	public IntVector2 GetCoordinates()
	{
		return currentCoordinates;
	}

	public void SetInfo(int x, int y, Game game)
	{
		currentCoordinates = new IntVector2(x, y);
		parentGame = game;
	}
}