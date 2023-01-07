using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectorButton : MonoBehaviour
{
	public bool IsSelected { get; private set; }

	[SerializeField] private Image[] _images;
	[SerializeField] private GameObject _playerFrame; 
	[SerializeField] private TextMeshProUGUI _playerNumber;
	public Color Color { get; private set; }

	public void SetColor(Color c)
	{
		Color = c;
		Array.ForEach(_images, s => s.color = c);
	}

	public bool TrySelect(int playerIndex)
	{
		if (IsSelected)
			return false;

		IsSelected = true;
		_playerNumber.text = $"P{playerIndex + 1}";
		_playerFrame.gameObject.SetActive(true);

		return true;
	}

	public void Unselect()
	{
		IsSelected = false;

		_playerFrame.gameObject.SetActive(false);
	}
}
