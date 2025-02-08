using SQLite4Unity3d;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
  private SQLiteConnection _connection;
  private int _lastSavedPlayerId;

  void Start()
  {

    string databasePath = Application.streamingAssetsPath + "/OntarioDriver.db";

    _connection = new SQLiteConnection(databasePath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);

    _connection.CreateTable<Player>();
  }

  public void SavePlayer(string name)
  {
    var player = new Player { Name = name };

    _connection.Insert(player);

    _lastSavedPlayerId = player.Id;

    Debug.Log($"Player '{name}' saved successfully.");
  }

  public string FetchPlayer()
  {
    var player = _connection.Table<Player>().Where(p => p.Id == _lastSavedPlayerId).FirstOrDefault();

    return player != null ? player.Name : "No player found with the specified ID.";
  }

  void OnDestroy()
  {
    _connection?.Close();
  }
}