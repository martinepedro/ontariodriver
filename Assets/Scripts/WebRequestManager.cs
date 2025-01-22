using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class WebRequestManager
{
  private string apiUrl;

  public WebRequestManager(string apiUrl)
  {
    this.apiUrl = apiUrl;
  }

  public void FetchCatFact(MonoBehaviour runner, Action<string> onSuccess, Action<string> onError)
  {
    runner.StartCoroutine(GetCatFact(onSuccess, onError));
  }

  private IEnumerator GetCatFact(Action<string> onSuccess, Action<string> onError)
  {
    using (UnityWebRequest request = UnityWebRequest.Get(apiUrl))
    {

      yield return request.SendWebRequest();

      if (request.result == UnityWebRequest.Result.Success)
      {
        CatFactResponse response = JsonUtility.FromJson<CatFactResponse>(request.downloadHandler.text);
        onSuccess?.Invoke(response.fact);
      }
      else
      {
        onSuccess?.Invoke(request.error);
      }
    }

  }
}

[System.Serializable]
public class CatFactResponse
{
  public string fact;
}
