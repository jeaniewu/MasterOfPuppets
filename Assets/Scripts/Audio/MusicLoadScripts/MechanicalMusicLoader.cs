﻿using UnityEngine;
using System.Collections;

public class MechanicalMusicLoader : MonoBehaviour {

	// Use this for initialization
	void Start () {
        ThemeMusicManager.getInstance().startMechanicalTheme();
	}
}
