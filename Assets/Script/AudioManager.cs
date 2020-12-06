using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class EffectAudio
{
    public string name;

    public AudioClip audioClip;
    private AudioSource source;
    public float volume;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = audioClip;
        source.volume = volume;
    }

    public void Play()
    {
        source.PlayOneShot(source.clip);
    }

}
public class AudioManager : MonoBehaviour
{
    [SerializeField]
    public EffectAudio[] audios;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i<audios.Length; i++)
        {
            GameObject soundObject = new GameObject("사운드 파일 이름: " + audios[i].name);
            audios[i].SetSource(soundObject.AddComponent<AudioSource>());
            soundObject.transform.SetParent(this.transform);
        }
    }

   public void Play(string name)
    {
        for(int i = 0; i < audios.Length; i++)
        {
            if(audios[i].name == name)
            {
                audios[i].Play();
                return;
            }
        }
    }
}
