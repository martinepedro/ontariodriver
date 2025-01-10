using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuEvents1 : MonoBehaviour
{
    private UIDocument _document;
	private List<Button> _menuButtons = new List<Button>();
	private AudioSource _gemidao;

	private void Awake()
	{
		_document = GetComponent<UIDocument>();
		_menuButtons = _document.rootVisualElement.Query<Button>().ToList();
		_gemidao = GetComponent<AudioSource>();

		for (int i= 0; i < _menuButtons.Count; i++)
		{
			_menuButtons[i].RegisterCallback<ClickEvent>(OnAllButtonsClick);
		}
	}

	private void OnDisable()
	{
		for (int i = 0; i < _menuButtons.Count; ++i)
		{
			_menuButtons[i].UnregisterCallback<ClickEvent>(OnAllButtonsClick);
		}
	}

	private void OnAllButtonsClick(ClickEvent evt)
	{
		Debug.Log("Filipe has pressed the button.");
		_gemidao.Play();
	}
}
