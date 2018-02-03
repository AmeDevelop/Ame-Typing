using System.Collections.Generic;

public class MusicListModel
{
    public List<Music> music { get; set; }
}

public class Music
{
    public string id { get; set; }
    public string title { get; set; }
    public string artist { get; set; }
    public int level { get; set; }
    public string url { get; set; }
    public string time { get; set; }
}