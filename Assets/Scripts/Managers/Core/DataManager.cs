using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
    public Dictionary<int, Data.Stat> StatDic { get; private set; } = new Dictionary<int, Data.Stat>();

    public void Init()
    {
        StatDic = LoadJson<Data.StatData, int, Data.Stat>("StatData").MakeDict();
    }

    TLoader LoadJson<TLoader, Key, Value>(string path) where TLoader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<TLoader>(textAsset.text);
    }
}