using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Impression {

    private string ip;
    private string apiKey;
    private System.DateTime date;



	// Use this for initialization

    public Impression(string ip, string apiKey)
    {
        this.Ip = ip;
        this.ApiKey = apiKey;
        this.Date = System.DateTime.Now;
    }

    public string Ip
    {
        get
        {
            return ip;
        }

        private set
        {
            ip = value;
        }
    }

    public string ApiKey
    {
        get
        {
            return apiKey;
        }

        private set
        {
            apiKey = value;
        }
    }

    public DateTime Date
    {
        get
        {
            return date;
        }

        private set
        {
            date = value;
        }
    }



}
