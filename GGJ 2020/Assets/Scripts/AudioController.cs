using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public GameObject audioSource;

    void Start() {

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

            newSound.GetComponent<AudioSource>().clip = clip;
            newSound.GetComponent<AudioSource>().volume = volume;
            newSound.GetComponent<AudioSource>().pitch = Random.Range(minRange, maxRange);
            newSound.GetComponent<AudioSource>().Play();



            Destroy(newSound, clip.length);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
