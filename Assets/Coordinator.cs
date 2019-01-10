using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class Coordinator {

    // Use this for initialization
    private Queue<Impression> queue;
    private float nextCheckTime = 2f;
    private string url = "http://localhost:3000";
    private string add = "/unity/impression/add";
    private static Coordinator instance = new Coordinator();

    private Coordinator() {
        this.queue = new Queue<Impression>();
    }

    public static Coordinator getInstance() {
        if (instance == null) {
            instance = new Coordinator();
            return instance;
        } else {
            return instance;
        }
    }

    public void pushImpression(Impression impression)
    {
        this.queue.Enqueue(impression);
    }

    public IEnumerator Dequeue()
    {
        for (; ; )
        {
            if (queue.Count > 0)
            {
                Debug.Log("Impressions in queue " + queue.Count);
                Impression imp = queue.Dequeue();
                WWWForm form = new WWWForm();
                form.AddField("ip", imp.Ip);
                form.AddField("date", imp.Date.ToString("MM/dd/yyyy HH:mm:ss"));
                Debug.Log(imp.Date.ToString("MM/dd/yyyy HH:mm:ss"));
                form.AddField("apikey", imp.ApiKey);

                UnityWebRequest www = UnityWebRequest.Post(url + add, form);
                www.SetRequestHeader("comeFromUnity", "True");
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                yield return new WaitForSeconds(nextCheckTime);
            }
            else yield return new WaitForSeconds(nextCheckTime);
        }
    }
    /*
    void Start()
    {
        //this.queue = new Queue<Impression>();
        //StartCoroutine(Dequeue());
    }

    // Update is called once per frame
    void Update () {
		
	}*/
}
