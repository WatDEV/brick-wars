using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour {

    public Slider Slider;
	public Text Hp;

    public void UpdateValue(int hitPoints, int maxHitPoints)
    {
        Slider.value = hitPoints / (float)maxHitPoints;
		Hp.text = $"{hitPoints}/{maxHitPoints}";
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
            Camera.main.transform.rotation * Vector3.up);
    }
}
