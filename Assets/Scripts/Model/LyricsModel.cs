using System.Collections.Generic;

public class LyricsModel
{
    public string title { get; set; }
    public string artist { get; set; }
    public List<Page> pages { get; set; }
}

public class Page
{
    public int seq { get; set; }
    public string startTime { get; set; }
    public List<Line> lines { get; set; }
}

public class Line
{
    public int num { get; set; }
    public string lyric { get; set; }
    [System.Xml.Serialization.XmlIgnore]
    public string color { get; set; }
    [System.Xml.Serialization.XmlIgnore]
    public bool isMe { get; set; }
}
