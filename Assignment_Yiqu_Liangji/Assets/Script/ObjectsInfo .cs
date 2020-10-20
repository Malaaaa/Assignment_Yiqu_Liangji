using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectsInfo : MonoBehaviour
{

    public static ObjectsInfo _instance;
    public TextAsset objectsInfoListText;

    private Dictionary<int, ObjectInfo> objectInfoDictionary =
        new Dictionary<int, ObjectInfo>();

    void Awake()
    {
        _instance = this;
        ReadInfo();
    }

    public ObjectInfo GetObjectInfoById(int key)
    {
        ObjectInfo info = new ObjectInfo();
        objectInfoDictionary.TryGetValue(key, out info);
        return info;
    }
    private void ReadInfo()
    {
        string text = objectsInfoListText.text;
        string[] strArray = text.Split('\n');
        foreach (string str in strArray)
        {
            string[] proArray = str.Split(',');

            ObjectInfo info = new ObjectInfo();

            int id = int.Parse(proArray[0]);
            string name = proArray[1];
            string iconName = proArray[2];

            info.id=id;
            info.name=name;
            info.iconName=iconName;
            
            objectInfoDictionary.Add(id, info);
        }
    }
}
//id,name,iconname
public class ObjectInfo
{
    public int id;
    public string name;
    public string iconName;

}