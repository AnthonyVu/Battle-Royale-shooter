﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HighnoonTools;

public class Api : MonoBehaviour {

	public HighnoonManager API { get; set; }

	// Use this for initialization
	void Start () {
		API = new HighnoonManager(R.Net.API_ADDRESS);
		DontDestroyOnLoad(this.gameObject);
	}
}
