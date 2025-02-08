using SQLite4Unity3d;

public class Player
{
  [PrimaryKey, AutoIncrement]
  public int Id { get; set; }

  public string Name { get; set; }
  public int HighScore { get; set; }
}