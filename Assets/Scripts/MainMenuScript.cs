using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuScript : MonoBehaviour
{
    private Button _quizButton, _simulatorButton, _exitButton;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        var UIDocument = GetComponent<UIDocument>();
        var root = UIDocument.rootVisualElement;

        _quizButton = root.Q<Button>("QuizButton");
        _simulatorButton = root.Q<Button>("SimulatorButton");
        _exitButton = root.Q<Button>("ExitButton");

        _quizButton.clicked += OnQuizButtonClicked;
        _simulatorButton.clicked += OnSimulatorButtonClicked;
		_exitButton.clicked += OnExitButtonClicked;
	}

	void Update()
    {
        
    }

    private void OnQuizButtonClicked()
    {
        SceneManager.LoadScene("Quiz");
		throw new NotImplementedException();
	}

	private void OnSimulatorButtonClicked()
	{
		throw new NotImplementedException();
	}
	private void OnExitButtonClicked()
	{
		Application.Quit();
#if UNITY_EDITOR
		EditorApplication.isPlaying = false;
#endif
	}
}
