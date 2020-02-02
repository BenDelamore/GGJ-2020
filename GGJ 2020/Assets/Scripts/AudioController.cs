using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeData
{
    float timeTotal;
    float initialVolume;
    float targetVolume;
    public AudioSource sound;

    public bool Initialize(AudioSource _sound, float _timeTotal, float _targetVolume)
    {
        if (_sound)
        {
            if (_sound.volume == _targetVolume)
            {
                return false;
            }
            if (_timeTotal == 0.0f)
            {
                Debug.Log("Can't fade over 0 seconds");
                return false;
            }

            sound = _sound;
            initialVolume = _sound.volume;
            targetVolume = _targetVolume;
            timeTotal = _timeTotal;
        }
        return false;
    }

    // Fades audio source by amount
    public bool PartialFade(float amount)
    {
        float deltaVolume = (targetVolume - initialVolume) * (amount / timeTotal);

        sound.volume += deltaVolume;

        if (Mathf.Abs(sound.volume) > Mathf.Abs(targetVolume))
        {
            sound.volume = targetVolume;
        }

        if (sound.volume == targetVolume)
        {
            return true;
        }


        return false;
    }
}


public class AudioController : MonoBehaviour
{
    public GameObject audioSource;

    public List<FadeData> currentFades;

    private float[] defaultRange;

    void Start() {
        currentFades = new List<FadeData>();
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

    public void StopAudio(AudioSource _sound)
    {
        foreach (FadeData fade in currentFades)
        {
            if (fade.sound == _sound)
            {
                currentFades.Remove(fade);
                _sound.Stop();
                // Stops the function from adding a second fade
                return;
            }
        }

        _sound.Stop();
    }

    public void FadeAudio(AudioSource _sound, float _targetVolume, float _totalTime)
    {
        FadeData newFade = new FadeData();
        foreach (FadeData fade in currentFades)
        {
            if (fade.sound == _sound)
            {
                // Stops the function from adding a second fade
                return;
            }
        }
        if (newFade.Initialize(_sound, _targetVolume, _totalTime))
        {
            currentFades.Add(newFade);
        }
    }

    // Update is called once per frame
    void Update()
    {
        int fadeCount = currentFades.Count;
        int i = 0;
        while (i < fadeCount)
        {
            // if the fade is done
            if (currentFades[i].PartialFade(Time.deltaTime))
            {
                currentFades.RemoveAt(i);
                fadeCount--;
            }
        }
    }
}
