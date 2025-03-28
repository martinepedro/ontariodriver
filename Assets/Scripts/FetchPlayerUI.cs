using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FetchPlayerUI : MonoBehaviour
{
  public TMP_InputField nameInputField;
  public Button saveButton;
  public Button fetchButton;
  public TMP_Text nameText;

  private DatabaseManager _databaseManager;

  void Start()
  {
    _databaseManager = FindFirstObjectByType<DatabaseManager>();
    if (_databaseManager == null)
    {
      Debug.LogError("DatabaseManager not found in the scene!");
    }

    if (saveButton == null)
    {
      Debug.LogError("saveButton is not assigned!");
    }

    if (fetchButton == null)
    {
      Debug.LogError("fetchButton is not assigned!");
    }

    if (nameText == null)
    {
      Debug.LogError("nameText is not assigned!");
    }

    saveButton.onClick.AddListener(SaveUser);
    fetchButton.onClick.AddListener(FetchUser);

    fetchButton.interactable = false;

    nameText.text = "Name will appear here";
  }

  private void SaveUser()
  {
    string userName = nameInputField.text;

    if (!string.IsNullOrEmpty(userName))
    {

      _databaseManager.SavePlayer(userName);

      nameInputField.text = "";

      fetchButton.interactable = true;

      Debug.Log("User saved successfully!");
    }
    else
    {
      Debug.LogWarning("Please enter a valid name.");
    }
  }

  private void FetchUser()
  {
    if (nameText == null)
    {
      Debug.LogError("nameText is not assigned!");
      return;
    }

    string userName = _databaseManager.FetchPlayer();
    Debug.Log($"Fetched user name: {userName}");

    nameText.text = "";
    nameText.text = $"User: {userName}";
  }
}