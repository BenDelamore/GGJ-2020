using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public GameObject audioSource;

    public AudioClip ambience;
    public AudioClip click;
    public AudioClip hit1;
    public AudioClip hit2;
    public AudioClip interfaceSelect;
    public AudioClip notification;
    public AudioClip tap;
    public AudioClip thruster;
    public AudioClip win1;
    public AudioClip win2;
    public AudioClip win3;

    private float[] defaultRange;

    void Start() {
        defaultRange[0] = 1;
        defaultRange[1] = 1;
    }

    public void PlaySoundAt(AudioClip clip, Transform trans, float volume, float minRange = 1f, float maxRange = 1f, bool hasMultiple = false) {
        bool hasAudio = false;

        if (!hasMultiple) {
            foreach (Transform child in trans) {
                if (child.name.Contains("Audio")) {
                    hasAudio = true;
                    if (!child.GetComponent<AudioSource>().isPlaying) {
                        child.GetComponent<AudioSource>().Play();
                    }
                }
            }
        }

        if (hasAudio == false) {
            GameObject newSound = Instantiate(audioSource,trans);
            newSound.transform.SetPositionAndRotation(transform.position, transform.rotation);

            Destroy(newSound, clip.length);

            newSound.GetComponent<AudioSource>().clip = clip;
            newSound.GetComponent<AudioSource>().volume = volume;
            newSound.GetComponent<AudioSource>().pitch = Random.Range(minRange, maxRange);
            newSound.GetComponent<AudioSource>().Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
