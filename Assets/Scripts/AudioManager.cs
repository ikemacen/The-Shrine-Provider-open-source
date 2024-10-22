using UnityEngine.Audio;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    // Start is called before the first frame update
    public static AudioManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeSounds();
            Debug.Log("AudioManager instance created and set to DontDestroyOnLoad.");
        }
        else if (instance != this)
        {
            Debug.Log("Duplicate AudioManager found. Destroying the new instance.");
            Destroy(gameObject);
            return;
        }
    }
    private void InitializeSounds()
    {
        foreach (Sound s in sounds)
        {
            if (s.source == null)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
            }
        }
    }
    public void Start(){
        Play("Menu Theme");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Update is called once per frame
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null){
            Debug.LogWarning("Sound: " + name + " Not found!");
            return;
        }
        s.source.Play();
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }
    public bool IsPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return false;
        }
        return s.source.isPlaying;
    }
    public void PlayRandomSoundFromRange(int startIndex, int endIndex)
    {
        if (startIndex < 0 || endIndex >= sounds.Length || startIndex > endIndex)
        {
            Debug.LogWarning("Invalid range of indexes");
            return;
        }

        int randomIndex = UnityEngine.Random.Range(startIndex, endIndex + 1);
        Sound s = sounds[randomIndex];
        s.source.Play();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        if(scene.name == "Menu"){
            Play("Menu Theme");
            Stop("Theme");
        }else if(scene.name == "Playspace"){
            Play("Theme");
            Stop("Menu Theme");
        }
    }
    void OnDestroy(){
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
