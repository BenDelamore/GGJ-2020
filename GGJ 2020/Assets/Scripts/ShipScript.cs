using UnityEngine;

public class ShipScript : MonoBehaviour
{
    private Rigidbody2D thisRB;
    public GameObject cam;
    public AudioClip hitSound;
    public float health;
    public int gyroscopeCount = 0;
    public int warpCoreCount = 0;

    public float velocityMag;
   
    void Start()
    {
        health = 100.0f;
        thisRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (health <= 0.0f)
        {
            // game over
            FindObjectOfType<SceneSwitcher>().SceneSwitch("TitleScreen");
        }


        velocityMag = thisRB.velocity.magnitude;

        if (Input.GetKey("q") && !GetComponent<ShipBuilding>().isDragging)
        {
            thisRB.AddTorque(1000.0f + 500.0f * gyroscopeCount);
        }
        if (Input.GetKey("e") && !GetComponent<ShipBuilding>().isDragging)
        {
            thisRB.AddTorque(-(1000.0f + 500.0f * gyroscopeCount));
        }
        /*
        if (Input.GetKey("w"))
        {
            //Forwards();
            Move(0);
        }

        if (Input.GetKey("s"))
        {
            Move(2);
        }

        if (Input.GetKey("a"))
        {
            Move(1);
        }

        if (Input.GetKey("d"))
        {
            Move(3);
        }
        */
    }

    void Forwards()
    {
        foreach (Transform child in transform)
        {
            if (child.name.Contains("Thruster") && child.transform.localRotation.eulerAngles.z == 0.0f)
            {
                child.GetComponent<ThrusterScript>().Thrust();
            }
        }
    }

    // 0 = forwards, 1 = facing the left, 2 = backwards, 3 = facing the right
    void Move(int _direction)
    {
        
        foreach (Transform child in transform)
        {
            if (child.name.Contains("Thruster") && child.transform.localRotation.eulerAngles.z <= (_direction * 90.0f) + 5.0f && child.transform.localRotation.eulerAngles.z >= (_direction * 90.0f) - 5.0f)
            {
                child.GetComponent<ThrusterScript>().Thrust();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > 0.2f)
        {
            health -= 2.0f;
            float speedRatioMin = Mathf.Clamp(velocityMag / 100,0.25f,1);
            float speedRatioMax = Mathf.Clamp(speedRatioMin + 0.1f, speedRatioMin, 1);
            cam.GetComponent<AudioController>().PlaySoundAt(hitSound, transform, 0.5f, speedRatioMin, 1f,true);
        }
    }
}