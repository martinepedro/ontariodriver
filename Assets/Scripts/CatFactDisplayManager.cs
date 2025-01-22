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
    webRequestManager.FetchCatFact(this,

    onSuccess: (fact) =>
    {
      Debug.Log("Fetched Cat Fact: " + fact);
      catFactText.text = fact;
    },
    onError: (error) =>
    {
      Debug.LogError("Error fetching cat fact: " + error);
      catFactText.text = "Failed to load cat fact. Try again!";
    });
  }
}