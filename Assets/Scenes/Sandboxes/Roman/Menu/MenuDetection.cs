using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuDetection : BaseDetection
{
    public Button menuPressed;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    public override void OnDetectionEnter()
    {
        menuPressed.image.color = Color.grey;
    }

    public override void OnDetectionExit()
    {
        menuPressed.image.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
