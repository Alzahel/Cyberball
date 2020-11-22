using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cyberball.Managers
{
    //MADE WITH https://www.youtube.com/watch?v=HhFKtiRd0qI

    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;

        [Range(0,1)]
        public float volume = 1f;
        [Range(0, 1.5f)]
        public float pitch = 1f;

        [Range(0, 1)]
        public float randomVolume = 0f;
        [Range(0, 1.5f)]
        public float randomPitch = 0f;

        private AudioSource source;

        public AudioSource Source { get => source; set { source = value; source.clip = clip; } }

        public void SetSource (AudioSource _source)
        {
            source = _source;
        
        }

        public void Play()
        {
            source.volume = volume *(1+Random.Range(-randomVolume /2f, randomVolume /2f));
            source.pitch = pitch * (1 + Random.Range(-randomPitch / 2f, randomPitch / 2f));
            source.Play();
        }
    }

    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;

        [SerializeField]
        private Sound[] sounds = null;

        private void Awake()
        {
            if (instance != null) Debug.LogError("More than 1 audio Manager in the scene");
            else instance = this;
        }

        private void Start()
        {
            for (int i = 0; i < sounds.Length; i++)
            {
                GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].name);
                _go.transform.SetParent(this.transform);
                _go.AddComponent<AudioSource>();
                sounds[i].Source = _go.AddComponent<AudioSource>();
                sounds[i].Source.clip = sounds[i].clip;
            }
        }

        public void PlaySound(string _name)
        {
            for (int i = 0; i < sounds.Length; i++)
            {
                if (sounds[i].name == _name)
                {
                    sounds[i].Play();
                    return;
                }
            }

            //no sound found
            Debug.LogWarning("AudioManager : Sound not found :" + _name);
        }

        public void PlaySpatialSound(string _name, Vector3 position)
        {
            for (int i = 0; i < sounds.Length; i++)
            {
                if (sounds[i].name == _name)
                {
                    AudioSource audioSource = sounds[i].Source;
                    audioSource.maxDistance = 100f;
                    audioSource.spatialBlend = 1f;
                    audioSource.rolloffMode = AudioRolloffMode.Linear;
                    audioSource.dopplerLevel = 0f;
                    sounds[i].Play();
                    return;
                }
            }

            //no sound found
            Debug.LogWarning("AudioManager : Sound not found :" + _name);
        }
    }
}