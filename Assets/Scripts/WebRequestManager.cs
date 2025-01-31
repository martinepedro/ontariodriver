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

  public void FetchData(MonoBehaviour runner, Action<string> onSuccess, Action<string> onError)
  {
    runner.StartCoroutine(GetData(onSuccess, onError));
  }

  private IEnumerator GetData(Action<string> onSuccess, Action<string> onError)
  {
    using (UnityWebRequest request = UnityWebRequest.Get(apiUrl))
    {

      yield return request.SendWebRequest();

      if (request.result == UnityWebRequest.Result.Success)
      {
        onSuccess?.Invoke(request.downloadHandler.text);
      }
      else
      {
        onError?.Invoke(request.error);
      }
    }

  }
}