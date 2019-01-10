using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Advertisement : MonoBehaviour {

    // Use this for initialization
    public Texture2D DefaultTexture;

    private string url = "http://localhost:3000/unity";
    private string serve = "/serve";
    private string check = "/check";
    private int version = 0;

    private float nextCheckTime = 60f;

    void Start () {
        StartCoroutine(DoCheck());
    }

    IEnumerator UpdateTexture()
    {
        Texture2D tex;
        tex = new Texture2D(4, 4, TextureFormat.DXT1, false);
        WWW www = new WWW(url + serve);
        yield return www;
        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.error);
            GetComponent<Renderer>().material.mainTexture = DefaultTexture;
        }
        else
        {
            www.LoadImageIntoTexture(tex);
            GetComponent<Renderer>().material.mainTexture = tex;
        }
    }

    IEnumerator DoCheck ()
    {
        for (; ; )
        {
            WWW www = new WWW(url + check);
            yield return www;
            if (!string.IsNullOrEmpty(www.error))
            {
                Debug.Log(www.error);
            }
            else
            {
                int ver = int.Parse(www.text);
                if (this.version < ver)
                {
                    this.version = ver;
                    StartCoroutine(UpdateTexture());
                }
            }
            yield return new WaitForSeconds(nextCheckTime);
        }
    }


    // Update is called once per frame
    void Update () {

    }
}
