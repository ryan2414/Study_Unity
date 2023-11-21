using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    class Test
    {
        public int id = 0;

    }
    class CoroutineTest : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new Test { id = 1 };
            // break;
            yield return new Test { id = 2 };
            yield return new Test { id = 3 };
            yield return new Test { id = 4 };
        }
    }

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        Managers.UI.ShowSceneUI<UI_Inven>();

        CoroutineTest test = new CoroutineTest();
        foreach (var item in test)
        {
            Test value = (Test)item;
            Debug.Log(value.id);
        }
    }

    public override void Clear()
    {
        throw new System.NotImplementedException();
    }
}
