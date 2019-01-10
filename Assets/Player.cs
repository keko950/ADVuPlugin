using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    
    private Vector3 center;
    private Renderer m_Renderer;
    private float maxDistance = 500f;
    private float radius = 30f;
    private string url = "http://localhost:3000";
    private string playerIp;
    private bool isEnabled = false;
    private Coordinator coordinator;

    public string apiKey;
    // Use this for initialization
    void Start () {
        m_Renderer = GetComponent<Renderer>();
        coordinator = Coordinator.getInstance();
        StartCoroutine(getIp());
        StartCoroutine(coordinator.Dequeue());
    }

    IEnumerator getIp()
    {
        //this should be changed in production
        //WWW www = new WWW(url + "/unity/userIp");
        WWW www = new WWW("https://api.ipify.org/");
        yield return www;
        playerIp = www.text;
        Debug.Log("Impression from " + playerIp);
    }

    IEnumerator wait5Minutes()
    {
        yield return new WaitForSeconds(5);
        this.isEnabled = false;
    }
    private void FixedUpdate()
    {

    }
    // Update is called once per frame
    void Update () {
        if (!this.isEnabled)
        {
            center = this.GetComponent<Transform>().position;
            Collider[] hitColliders = Physics.OverlapSphere(center, radius);
            foreach (Collider collider in hitColliders)
            {
                GameObject go = collider.gameObject;
                if (go.GetComponent<Advertisement>())
                {
                    if (go.GetComponent<Renderer>().isVisible)
                    {
                        RaycastHit hit;
                        
                        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance) &&
                            hit.collider.gameObject.GetComponent<Advertisement>())

                        {
                            Impression impression = new Impression(this.playerIp, this.apiKey);
                            coordinator.pushImpression(impression);
                            this.isEnabled = true;
                            StartCoroutine(wait5Minutes());

                        }
                    }
                }
            }
        }
    }
}
