using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSound : MonoBehaviour
{
    public AudioClip _audio;
    public AudioClip _audio2;
    private void OnTriggerEnter(Collider other)
    {
        //AudioSource audio = GetComponent<AudioSource>();
        //audio.PlayOneShot(_audio);
        //audio.PlayOneShot(_audio2);

        //float lifeTime = Mathf.Max(_audio.length, _audio2.length);

        //GameObject.Destroy(gameObject, lifeTime);

        Managers.Sound.Play(Define.Sound.Effect, "UnityChan/univ1329");
        Managers.Sound.Play(Define.Sound.Effect, "UnityChan/univ1328");
    }
}
