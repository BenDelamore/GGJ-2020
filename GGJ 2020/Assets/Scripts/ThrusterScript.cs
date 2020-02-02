using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterScript : moduleBehaviour
{
    public float thrustForce;
    public string control = "w";
    public AudioClip thrusterSound;
    public GameObject cam;

    private void Update()
    {
        if (rootNode)
        {
            if (Input.GetKey(control))
            {
                Thrust();
            }
            else {
                foreach(Transform child in transform) {
                    if (child.name.Contains("Audio")) {
                        child.GetComponent<AudioSource>().Stop();
                        Camera.main.GetComponent<AudioController>().FadeAudio(child.GetComponent<AudioSource>(), 0.0f, 0.5f);
                    }
                }
            }
            //if (Input.GetKey("w") && transform.localRotation.eulerAngles.z <= 5.0f && transform.localRotation.eulerAngles.z >= -5.0f)
            //{
            //    Thrust();
            //}
            //if (Input.GetKey("w") && transform.forward == GameObject.Find("Core").transform.forward)
            //{
            //    Thrust();
            //}
            //if (Input.GetKey("a") && transform.localRotation.eulerAngles.z <= 95.0f && transform.localRotation.eulerAngles.z >= 85.0f)
            //{
            //    Thrust();
            //}
            //if (Input.GetKey("a") && transform.forward == -GameObject.Find("Core").transform.right)
            //{
            //    Thrust();
            //}
            //if (Input.GetKey("s") && transform.localRotation.eulerAngles.z <= -175.0f && transform.localRotation.eulerAngles.z >= 175.0f)
            //{
            //    Thrust();
            //}
            //if (Input.GetKey("s") && transform.forward == -GameObject.Find("Core").transform.forward)
            //{
            //    Thrust();
            //}
            //if (Input.GetKey("d") && transform.localRotation.eulerAngles.z <= -85.0f && transform.localRotation.eulerAngles.z >= -95.0f)
            //{
            //    Thrust();
            //}
            //if (Input.GetKey("d") && transform.forward == GameObject.Find("Core").transform.right)
            //{
            //    Thrust();
            //}
        }
    }

    public void Thrust()
    {
        cam.GetComponent<AudioController>().PlaySoundAt(thrusterSound,transform,0.5f);

        GetComponent<Rigidbody2D>().AddForce(transform.up * thrustForce * Time.deltaTime);
        health -= 0.125f;
        if (health <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}