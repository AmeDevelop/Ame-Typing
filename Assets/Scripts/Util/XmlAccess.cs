using System;
using System.IO;
using System.Text;

public class XmlAccess
{
    public System.Xml.Serialization.XmlSerializer serializer;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="type"></param>
    public XmlAccess(Type type)
    {
        serializer = new System.Xml.Serialization.XmlSerializer(type);
    }

    /// <summary>
    /// XMLファイルを読み込み
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public object xmlRead(string path)
    {
        StreamReader sr = new StreamReader(path, new UTF8Encoding(false));
        object buf = serializer.Deserialize(sr);
        sr.Close();
        return buf;
    }

    /// <summary>
    /// XMLファイルに書き込み
    /// </summary>
    /// <param name="path"></param>
    /// <param name="buf"></param>
    /// <param name="append"></param>
    /// <returns></returns>
    public void xmlWrite(string path, object buf, bool append)
    {
        StreamWriter sw = new StreamWriter(
            path, append, new System.Text.UTF8Encoding(false));
        serializer.Serialize(sw, buf);
        sw.Close();
    }
}
