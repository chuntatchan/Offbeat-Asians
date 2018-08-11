using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour {

	[SerializeField]
	private Image itemImage;
	[SerializeField]
	private Image colorImage;
	[SerializeField]
	private Sprite itemColor;

	public void SetItemImage(Sprite _itemImage) {
		itemImage.sprite = _itemImage;
	}

	public void SetColor(Sprite _newColor) {
		itemColor = _newColor;
		colorImage.sprite = _newColor;
	}

	public Sprite GetItemSprite() {
		return itemImage.sprite;
	}

}
