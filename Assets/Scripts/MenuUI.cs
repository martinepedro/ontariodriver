using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuUI : MonoBehaviour
{
	public TextAsset QuestionsJson;

	private Quiz _quiz;
	private int _currentQuestion = -1;
	private bool awaitingAnswer = false;

	// UI
	private Button[] _buttons;
	private Label _questionText;
	private VisualElement _questionImage;
	private Texture2D _image;
	private ProgressBar _progressBar;
	private Label _progressLabel;


	private void Start()
	{
		VisualElement root = GetComponent<UIDocument>().rootVisualElement;

		_buttons = new Button[]
		{
			root.Q<Button>("Button1"),
			root.Q<Button>("Button2"),
			root.Q<Button>("Button3"),
			root.Q<Button>("Button4")
		};
		_buttons[0].clicked += () => CheckAnswer(1);
		_buttons[1].clicked += () => CheckAnswer(2);
		_buttons[2].clicked += () => CheckAnswer(3);
		_buttons[3].clicked += () => CheckAnswer(4);

		_questionText = root.Q<Label>("QuestionText");
		_questionImage = root.Q<VisualElement>("QuestionImage");
		_progressLabel = root.Q<Label>("ProgressLabel");
		_progressBar = root.Q<ProgressBar>("ProgressBar");
		_progressBar.highValue = 30;

		LoadQuiz();

		StartCoroutine(ManageGame());
	}

	private IEnumerator ManageGame()
	{
		int progress = 0;
		while (progress < 30)
		{
			_progressBar.value = progress;
			_progressLabel.text = "Question " + (progress + 1) + "/30";
			awaitingAnswer = true;
			DisplayNextQuestion();
			while (awaitingAnswer) yield return null;
			progress++;
		}
	}

	void LoadQuiz()
	{
		if (QuestionsJson != null)
		{
			_quiz = JsonUtility.FromJson<Quiz>(QuestionsJson.text);
			if (_quiz.Questions != null)
			{
				Debug.Log($"Quiz loaded with {_quiz.Questions.Count} questions.");
			}
			else
			{
				Debug.LogError("Failed to load quiz data.");
			}
		}
		else
		{
			Debug.LogError("JSON file not assigned in the inspector.");
		}
	}

	private void DisplayNextQuestion()
	{
		if (_quiz.Questions.Count == 0) return;
		if (_quiz.Questions.Count == _currentQuestion) return;

		if (_currentQuestion < 0) // Initializes the quiz
			_currentQuestion = 0;
		else
			_currentQuestion++;

		LoadQuestion(_quiz.Questions[_currentQuestion]);
	}

	private void LoadQuestion(QuizQuestion question)
	{
		_buttons[0].text = question.One;
		_buttons[1].text = question.Two;
		_buttons[2].text = question.Three;
		_buttons[3].text = question.Four;

		_questionText.text = question.QuestionText;
		if (!string.IsNullOrEmpty(question.Image))
		{
			StyleBackground sprite = new StyleBackground(Resources.Load<Sprite>(question.Image));
			_questionImage.style.backgroundImage = sprite;
		}

		return;
	}

	bool CheckAnswer(int buttonIndex)
	{
		if (_currentQuestion < 0) return false;
		
		awaitingAnswer = false;
		return buttonIndex == _quiz.Questions[_currentQuestion].Answer;
	}
}


[System.Serializable]
public class QuizQuestion
{
	public string One, Two, Three, Four;
	public string Image;
	public int Answer;
	public string QuestionText;
}

[System.Serializable]
public class Quiz
{
	public List<QuizQuestion> Questions;
}
