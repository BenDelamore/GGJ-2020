using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moduleBehaviour : MonoBehaviour
{
    public float health;
    public GameObject UIelement;
    public GameObject rootNode;
    public AudioClip popOff;

    public GameObject CanConnect(GameObject _node, float rotation)
    {
        Transform oldParent = transform.parent;
        Vector3 oldLocalPosition = transform.localPosition;
        Quaternion oldLocalRotation = transform.localRotation;

        transform.parent = _node.GetComponent<nodeScript>().rootObject.transform;
        transform.localPosition = _node.transform.localPosition;
        transform.localRotation = Quaternion.Euler(0, 0, rotation);

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if (child.tag == "SnapNode")
            {
                // Get root object's non-trigger collider
                Collider2D trueCollider = null;
                foreach (Collider2D collider in _node.GetComponent<nodeScript>().rootObject.GetComponents<Collider2D>())
                {
                    if (!collider.isTrigger)
                    {
                        trueCollider = collider;
                        break;
                    }
                }
                if (trueCollider.bounds.Contains(child.transform.position))
                {
                    transform.parent = oldParent;
                    transform.localPosition = oldLocalPosition;
                    transform.localRotation = oldLocalRotation;
                    return child;
                }
            }
        }

        transform.parent = oldParent;
        transform.localPosition = oldLocalPosition;
        transform.localRotation = oldLocalRotation;
        return null;
    }


    public void Connect(GameObject _node, float rotation)
    {
        // THE NODE OF THIS MODULE THAT WILL OVERLAP THE ROOT OBJECT OF _node
        GameObject overlapNode = CanConnect(_node, rotation);
        overlapNode.GetComponent<nodeScript>().boundObject = _node.GetComponent<nodeScript>().rootObject;
        if (overlapNode.GetComponent<nodeScript>().boundObject != GameObject.Find("Core"))
        {
            overlapNode.GetComponent<nodeScript>().boundObject.GetComponent<moduleBehaviour>().rootNode = overlapNode;

        }
        _node.GetComponent<nodeScript>().boundObject = gameObject;
        rootNode = _node;
    }

    public void Disconnect()
    {
        if (rootNode)
        {
            nodeScript[] nodeScripts = rootNode.GetComponent<nodeScript>().boundObject.GetComponentsInChildren<nodeScript>();
            foreach (nodeScript script in nodeScripts)
            {
                if (script.boundObject == gameObject)
                {
                    script.boundObject = null;
                    break;
                }
            }
            rootNode.GetComponent<nodeScript>().boundObject = null;
            rootNode = null;
        }
        nodeScript[] nodes = GetComponentsInChildren<nodeScript>();
        foreach (nodeScript script in nodes)
        {
            if (script.boundObject)
            {
                nodeScript[] nodeScriptsInBound = script.boundObject.GetComponentsInChildren<nodeScript>();
                foreach (nodeScript scriptInBound in nodeScriptsInBound)
                {
                    if (scriptInBound.boundObject == gameObject)
                    {
                        scriptInBound.boundObject = null;
                        break;
                    }
                }
                script.boundObject = null;
                break;
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        health = 100.0f;
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if (child.tag == "HealthUI")
            {
                UIelement = child;
            }
        }
        UIelement.transform.SetParent(GameObject.Find("Canvas").transform);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 worldToScreen = Camera.main.WorldToScreenPoint(transform.position);
        GameObject canvas = GameObject.Find("Canvas");
        worldToScreen.x -= canvas.GetComponent<Canvas>().pixelRect.width * 0.5f;
        worldToScreen.y -= canvas.GetComponent<Canvas>().pixelRect.height * 0.5f;
        UIelement.transform.localPosition = worldToScreen;
    }

    private void OnMouseEnter()
    {
        UIelement.transform.GetComponent<ModHealthUI>().showHealth = true;
    }

    private void OnMouseExit()
    {
        UIelement.transform.GetComponent<ModHealthUI>().showHealth = false;
    }


    private void OnDestroy()
    {
        Disconnect();
        Destroy(UIelement);
    }
}
