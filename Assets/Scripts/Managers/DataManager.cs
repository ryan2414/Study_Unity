using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
    public Dictionary<int, Stat> StatDic { get; private set; } = new Dictionary<int, Stat>();

    public void Init()
    {
        StatDic = LoadJson<StatData, int, Stat>("StatData").MakeDict();
    }

    TLoader LoadJson<TLoader, Key, Value>(string path) where TLoader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<TLoader>(textAsset.text);
    }
}