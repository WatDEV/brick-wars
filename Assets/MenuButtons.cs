﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour {

	public void Exit()
    {
        Application.Quit();
    }

    public void LoadBattleScene()
    {
        SceneManager.LoadScene("Battle");
    }
}
