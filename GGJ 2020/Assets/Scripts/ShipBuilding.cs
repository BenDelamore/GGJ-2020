using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBuilding : MonoBehaviour
{
    public bool isDragging = false;
    public GameObject cam;
    public AudioClip connectSound;
    public AudioClip popOff;
    bool snapToShip;
    GameObject draggedObject;
    float rotation;
    GameObject closestNode;

    float shipOffset = 1.28f;

    void LateUpdate() {
        Time.timeScale = 1;

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            DragStop();
        }

        if (isDragging)
        {
            Time.timeScale = 0.1f;
            Dragging();
        }
    }

    public void DragStart(GameObject part) {
        if (isDragging == false) {

            isDragging = true;
            draggedObject = part;

            if (draggedObject.transform.parent != null) {
                Debug.Log("pop!");
                Debug.Log(draggedObject.transform.parent);
                Camera.main.GetComponent<AudioController>().PlaySoundAt(popOff, draggedObject.transform, 0.5f, 1, 1, true);
            }

            draggedObject.GetComponent<FixedJoint2D>().enabled = false;
            draggedObject.GetComponent<BoxCollider2D>().enabled = false;
            draggedObject.transform.parent = null;

            if (draggedObject.GetComponent<GyroscopeScript>())
            {
                if (draggedObject.GetComponent<GyroscopeScript>().rootNode)
                {
                    GetComponent<ShipScript>().gyroscopeCount--;
                }
            }
            if (draggedObject.GetComponent<WarpCoreScript>())
            {
                if (draggedObject.GetComponent<WarpCoreScript>().rootNode)
                {
                    GetComponent<ShipScript>().warpCoreCount--;
                }
            }
            // tells the dragged object to Disconnect itself from the node
            draggedObject.GetComponent<moduleBehaviour>().Disconnect();
        }
    }

    void Dragging() {
        snapToShip = false;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);

        draggedObject.transform.position = mousePos;

        /*
        //check if near children
        foreach (Transform child in transform) {
            if (Within(mousePos, child.transform.position, 3.75f, 3.75f) && child.gameObject != draggedObject) {
                snapToShip = true;
            }
            else {
                if (snapToShip == false) {
                    snapToShip = false;
                }
            }
        }

        //check if near core
        if (snapToShip == false && Within(mousePos, transform.position + transform.up * shipOffset, 3.75f, 5.12f) == true) {
            snapToShip = true;
        }
        */


        if (Input.GetKeyDown("q"))
        {
            rotation += 90.0f;
            if (rotation == 450.0f)
            {
                rotation = 90.0f;
            }
        }
        if (Input.GetKeyDown("e"))
        {
            rotation -= 90.0f;
            if (rotation == -90.0f)
            {
                rotation = 270.0f;
            }
        }

        draggedObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotation);


        GameObject[] snapNodes = GameObject.FindGameObjectsWithTag("SnapNode");
        closestNode = null;
        float closestDistance = Mathf.Infinity;
        foreach (GameObject node in snapNodes)
        {
            float distance = (draggedObject.transform.position - node.transform.position).magnitude;
            if ((closestNode == null || distance < closestDistance) && !node.GetComponent<nodeScript>().boundObject)
            {
                if (draggedObject.GetComponent<moduleBehaviour>().CanConnect(node, rotation))
                {
                    closestNode = node;
                    closestDistance = distance;
                }
            }
        }

        if (closestDistance <= 3.75f)
        {
            snapToShip = true;
        }

        if (snapToShip) {
            Snap();
        }
        else {
            draggedObject.transform.parent = null;
        }
    }

    void DragStop() {
        isDragging = false;
        
        bool repaired = false;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        /*
        foreach (Transform child in transform) {
            if (Within(mousePos, child.transform.position, 1.25f, 1.25f) && child.gameObject != draggedObject) {
                repair(child.gameObject);
                repaired = true;
                break;
            }
        }
        if (repaired == false) {
            if (Within(mousePos, transform.position + transform.up * shipOffset, 1.5f, 3f)) {
                repair(gameObject);
                repaired = true;
            }
        }
        */
        
        if (snapToShip) {
            //Play Connection Sound
            cam.GetComponent<AudioController>().PlaySoundAt(connectSound, transform, 0.5f,1,1,true);
            
            // get the closest node to the mouse position and set the object to it's position

            /*
            GameObject[] snapNodes = GameObject.FindGameObjectsWithTag("SnapNode");
            GameObject closest = null;
            float closestDistance = Mathf.Infinity;
            foreach (GameObject node in snapNodes)
            {
                float distance = (draggedObject.transform.position - node.transform.position).magnitude;
                if ((closest == null || distance < closestDistance) && !node.GetComponent<nodeScript>().boundObject)
                {
                    closest = node;
                    closestDistance = distance;
                }
            }
            */


            // Sets the core as the parent
            Snap();
            draggedObject.GetComponent<FixedJoint2D>().enabled = true;
            draggedObject.GetComponent<FixedJoint2D>().connectedBody = closestNode.transform.parent.GetComponent<Rigidbody2D>();

            // Updates the node
            nodeScript closestNodeScript = closestNode.GetComponent<nodeScript>();
            closestNodeScript.boundObject = draggedObject;

            GameObject node = draggedObject.GetComponent<moduleBehaviour>().CanConnect(closestNode, rotation);
            node.GetComponent<nodeScript>().boundObject = closestNode.transform.parent.gameObject;

            draggedObject.GetComponent<moduleBehaviour>().Connect(closestNode, rotation);

            draggedObject.GetComponent<BoxCollider2D>().enabled = true;

            if (draggedObject.GetComponent<GyroscopeScript>())
            {
                GetComponent<ShipScript>().gyroscopeCount++;
            }
            if (draggedObject.GetComponent<WarpCoreScript>())
            {
                GetComponent<ShipScript>().warpCoreCount++;
            }
            if (draggedObject.GetComponent<ThrusterScript>())
            {
                if (rotation == 90.0f)
                {
                    draggedObject.GetComponent<ThrusterScript>().control = "a";
                }
                else if (rotation == 180.0f)
                {
                    draggedObject.GetComponent<ThrusterScript>().control = "s";
                }
                else if (rotation == 270.0f)
                {
                    draggedObject.GetComponent<ThrusterScript>().control = "d";
                }
            }
        }


        foreach (Collider2D collider in draggedObject.GetComponents<Collider2D>())
        {
            if (!collider.isTrigger)
            {
                collider.enabled = true;
                break;
            }
        }

        rotation = 0.0f;
    }

    void Snap() {
        /*
        GameObject[] snapNodes = GameObject.FindGameObjectsWithTag("SnapNode");
        GameObject closest = null;
        float closestDistance = Mathf.Infinity;
        foreach (GameObject node in snapNodes)
        {
            float distance = (draggedObject.transform.position - node.transform.position).magnitude;
            if (closest == null || distance < closestDistance)
            {
                closest = node;
                closestDistance = distance;
            }
        }
        */

        //Vector3 gridPos = new Vector3(Mathf.Round(draggedObject.transform.localPosition.x / 2.56f) * 2.56f, Mathf.Round((draggedObject.transform.localPosition.y) / 2.56f) * 2.56f);

        draggedObject.transform.parent = closestNode.transform.parent;
        draggedObject.transform.localPosition = closestNode.transform.localPosition;
        

        // All ogjects face forward
        draggedObject.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, rotation);

    }

    void repair(GameObject target) {
        Debug.Log("repaired " + target.name);
        Destroy(draggedObject.GetComponent<ThrusterScript>().UIelement);
        Destroy(draggedObject);
    }

    bool Within(Vector3 a, Vector3 b, float xMax, float yMax) {
        bool isWithin = false;

        if (Mathf.Abs(a.x - b.x) <= xMax && Mathf.Abs(a.y - b.y) <= yMax) {
            isWithin = true;
        }

        return isWithin;
    }
}
