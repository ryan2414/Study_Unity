using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    class Test
    {
        public int id = 0;

    }

    // 1. 함수의 상태를 저장/복원 가능!
        // -> 엄청 오래 걸리는 작업을 잠시 끊거나
        // -> 원하는 타이밍에 함수를 잠시 Stop/복원하는 경우
    // 2. return -> 우리가 원하는 타입으로 가능 (class도 가능)
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

        void GenerateItem()
        {
            // 아이템을 만들어준다.
            // DB 저장.

            // 멈춤
            // 로직
        }

        float deltaTime = 0;
        void ExplodeAfter4Seconds()
        {
            deltaTime += Time.deltaTime;
            if (deltaTime >= 4)
            {
                // 로직
            }
        }
    }

    Coroutine co;

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        Managers.UI.ShowSceneUI<UI_Inven>();

        //CoroutineTest test = new CoroutineTest();
        //foreach (var item in test)
        //{
        //    Test value = (Test)item;
        //    Debug.Log(value.id);
        //}

        co = StartCoroutine(ExplodeAfterSeconds(4));
        StartCoroutine((CoStopExplode(2)));
    }

    IEnumerator CoStopExplode(float seconds)
    {
        Debug.Log("Stop Enter!");
        yield return new WaitForSeconds(seconds);
        Debug.Log("Stop Execute!");

        if (co != null)
        {
            StopCoroutine(co);
            co = null;
        }
    }

    IEnumerator ExplodeAfterSeconds(float seconds)
    {
        Debug.Log("Explode Enter!");
        yield return new WaitForSeconds(seconds);
        Debug.Log("Explode Execute!!!!!");
        co = null;
    }

    public override void Clear()
    {
        throw new System.NotImplementedException();
    }
}
