using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    static private AudioManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
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

    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "Level1" && SceneManager.GetActiveScene().name != "Level2" && SceneManager.GetActiveScene().name != "Level3")
        {
            Destroy(this.gameObject);
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
