using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGmusic : MonoBehaviour
{

    private AudioSource audioSource;
    private GameObject[] musics;

    private void Awake()
    {
        musics = GameObject.FindGameObjectsWithTag("Music");
        if (musics.Length >=2)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(transform.gameObject);
        audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    public void PlayMusic()

    {
        if (audioSource.isPlaying) return;
        audioSource.Play();
    }

    // Update is called once per frame
    public void StopMusic()
    {
        audioSource.Stop();
    }
}
