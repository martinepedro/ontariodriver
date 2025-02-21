//using UnityEngine;
//using System.Collections.Generic;
//using UnityEngine.UIElements;
//using System;

//[System.Serializable]
//public class QuizOption
//{
//	public string text;
//}

//[System.Serializable]
//public class QuizQuestion
//{
//	public string question;
//	public List<QuizOption> options;
//	public int correct_answer;
//	public string image;
//}

//[System.Serializable]
//public class Quiz
//{
//	public List<QuizQuestion> quiz;
//}

//public class QuizController : MonoBehaviour
//{
//	public TextAsset jsonFile; // public opens up the field to be drag and dropped
//	public UIDocument quizUIDocument;

//	private Quiz quizData;
//	private VisualElement root;
//	private VisualElement optionsContainer;
//	private Label questionText;
//	private Button submitButton;
//	private QuizQuestion currentQuestion;

//	void Start()
//	{
//		root = null;// quizUIDocument.rootVisualElement;
//		questionText = root.Q<Label>("QuestionText");
//		optionsContainer = root.Q<VisualElement>("OptionsContainer");
//		submitButton = root.Q<Button>("SubmitAnswer");

//		LoadQuiz();
//		DisplayNextQuestion();
//	}

//	void LoadQuiz()
//	{
//		if (jsonFile != null)
//		{
//			quizData = JsonUtility.FromJson<Quiz>("{\"quiz\":" + jsonFile.text + "}");
//			if (quizData.quiz != null)
//			{
//				Debug.Log($"Loaded {quizData.quiz.Count} questions.");
//				// Here you can start processing the quiz data, like displaying questions, etc.
//			}
//			else
//			{
//				Debug.LogError("Failed to load quiz data. Check JSON format.");
//			}
//		}
//		else
//		{
//			Debug.LogError("JSON file not assigned in the inspector.");
//		}
//	}

//	void DisplayNextQuestion()
//	{
//		if (quizData.quiz.Count == 0) return;

//		currentQuestion = quizData.quiz[0];
//		quizData.quiz.RemoveAt(0);

//		questionText.text = currentQuestion.question;
//		optionsContainer.Clear();
//		for (int i = 0; i < currentQuestion.options.Count; i++)
//		{
//			Button optionButton = new Button();
//			optionButton.text = currentQuestion.options[i].text;
//			int optionIndex = i;
//			optionButton.clicked += () =>
//			{
//				CheckAnswer(optionIndex);
//			};
//			optionsContainer.Add(optionButton);
//		}
//	}

//	void CheckAnswer(int selectedIndex)
//	{
//		if (selectedIndex == currentQuestion.correct_answer)
//		{
//			Debug.Log("Correct!");
//		}
//		else
//		{
//			Debug.Log("Wrong! The correct answer was on button " + (currentQuestion.correct_answer + 1));
//		}
//		submitButton.SetEnabled(false);
//		if (quizData.quiz.Count > 0)
//		{
//			submitButton.clicked += () =>
//			{
//				DisplayNextQuestion();
//				submitButton.SetEnabled(true);
//			};
//		}
//		else
//		{
//			Debug.Log("Quiz Over!");
//		}
//	}	

//	public QuizQuestion GetRandomQuestion()
//	{
//		if (quizData != null && quizData.quiz.Count > 0)
//		{
//			return quizData.quiz[UnityEngine.Random.Range(0, quizData.quiz.Count)];
//		}
//		return null;
//	}

//}