using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private Slider slider;

    private float targtProgress;
    private float fillSpeed = 0.01f;


    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();    
    }

    // Start is called before the first frame update
    void Start()
    {
        IncrementProgress(1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (slider.value <targtProgress)
        {
            slider.value += fillSpeed * Time.deltaTime;
        }
    }

    public void IncrementProgress (float newProgress)
    {
        targtProgress = slider.value + newProgress;
    }


}
