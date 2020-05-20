using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderDetection : BaseDetection
{

    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnDetectionEnter()
    {
        button.image.color = Color.grey;
    }

    public override void OnDetectionExit()
    {
        button.image.color = Color.white;
    }
}
