using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSound : MonoBehaviour
{
    public AudioClip _audio;
    public AudioClip _audio2;

    int i = 0;
    private void OnTriggerEnter(Collider other)
    {
        //AudioSource audio = GetComponent<AudioSource>();
        //audio.PlayOneShot(_audio);
        //audio.PlayOneShot(_audio2);

        //float lifeTime = Mathf.Max(_audio.length, _audio2.length);

        //GameObject.Destroy(gameObject, lifeTime);

        i++;
        if (i % 2 == 0)
            Managers.Sound.Play("UnityChan/univ0001", Define.Sound.Bgm);
        else
            Managers.Sound.Play("UnityChan/univ0002", Define.Sound.Effect );
    }
}
