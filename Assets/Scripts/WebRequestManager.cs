using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class WebRequestManager : MonoBehaviour
{
  public TMPro.TextMeshProUGUI catFactText;

  private string apiUrl = "https://catfact.ninja/fact";

  private void Start()
  {
    FetchCatFact();
  }

  public void FetchCatFact()
  {
    StartCoroutine(GetCatFact());
  }

  private IEnumerator GetCatFact()
  {
    UnityWebRequest request = UnityWebRequest.Get(apiUrl);

    yield return request.SendWebRequest();

    if (request.result == UnityWebRequest.Result.Success)
    {
      CatFactResponse response = JsonUtility.FromJson<CatFactResponse>(request.downloadHandler.text);
      catFactText.text = response.fact;
    }
    else
    {
      Debug.LogError("Error fetching cat fact: " + request.error);
      catFactText.text = "Failed to load cat fact. Try again!";
    }

  }
}

[System.Serializable]
public class CatFactResponse
{
  public string fact;
}
