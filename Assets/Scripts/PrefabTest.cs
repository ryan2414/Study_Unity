using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabTest : MonoBehaviour
{
    GameObject prefab;
    GameObject tank;

    void Start()
    {
        tank = Managers.Resource.Instantiate("Tank");
        Managers.Resource.Destroy(tank, 3f);

        //prefab = Resources.Load<GameObject>("Prefabs/Tank");
        //tank = Instantiate(prefab);

        //Destroy(tank, 3f);
    }
}
