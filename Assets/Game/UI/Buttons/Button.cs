﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Button : MonoBehaviour 
{
	[SerializeField] protected Sprite upSprite;
	[SerializeField] protected Sprite downSprite;
	[SerializeField] protected SpriteRenderer spriteRenderer;
	[SerializeField] protected TextMeshPro text;

	protected virtual void OnEnable(){}

	public void Introduce(float delay)
	{
		LowerButton();
		Invoke("RaiseButton", delay);
	}
	protected virtual void RaiseButton() 
	{
		Vector3 pos = text.transform.localPosition;
		pos.y = 0f;
		text.transform.localPosition = pos;

		spriteRenderer.sprite = upSprite;
	}
	protected virtual void LowerButton()
	{
		Vector3 pos = text.transform.localPosition;
		pos.y = -0.1f;
		text.transform.localPosition = pos;

		spriteRenderer.sprite = downSprite;
	}

	protected virtual void OnMouseDown()
	{
		if (spriteRenderer.sprite == upSprite)
		{
			spriteRenderer.sprite = downSprite;

			Vector3 pos = text.transform.localPosition;
			pos.y -= 0.1f;
			text.transform.localPosition = pos;
		}
	}

	protected virtual void OnMouseUp()
	{
		if (spriteRenderer.sprite == downSprite)
		{
			spriteRenderer.sprite = upSprite;

			Vector3 pos = text.transform.localPosition;
			pos.y += 0.1f;
			text.transform.localPosition = pos;

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, Mathf.Infinity))
			{
				if (hit.collider.gameObject == this.gameObject)
				{
					OnPress();
				}
			}
		}
	}

	protected virtual void OnPress() {}
}