using UnityEngine;
using TMPro;

public class CatFactDisplayManager : MonoBehaviour
{
  public TextMeshProUGUI catFactText;

  private WebRequestManager webRequestManager;

  private void Start()
  {
    webRequestManager = new WebRequestManager("https://catfact.ninja/fact");

    FetchAndDisplayCatFact();
  }

  public void FetchAndDisplayCatFact()
  {
    webRequestManager.FetchData(this,

    onSuccess: (rawJson) =>
    {
      Debug.Log("Raw JSON: " + rawJson);
      GetResponse response = JsonUtility.FromJson<GetResponse>(rawJson);

      if (response != null && response.fact != null)
      {
        Debug.Log("Fetched Cat Fact: " + response.fact);
        catFactText.text = response.fact;
      }
      else
      {
        Debug.LogError("Error parsing cat fact from response.");
        catFactText.text = "Failed to parse cat fact.";
      }
    },
    onError: (error) =>
    {
      Debug.LogError("Error fetching cat fact: " + error);
      catFactText.text = "Failed to load cat fact. Try again!";
    });
  }
}

[System.Serializable]
public class GetResponse
{
  public string fact;
}