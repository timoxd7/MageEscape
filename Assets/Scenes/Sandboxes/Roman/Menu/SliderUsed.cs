using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderUsed : BaseInteraction
{

    public Slider slider;
    public GameObject button;
    
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnInteraction(InteractionContext context)
    {
        //Increases Value 
        if(button.name == "PlusButton") 
        {
            if(slider.value < 1.0f)
            {
                slider.value += 0.1f;
            }
        //Decreases Value
        } else if(button.name == "MinusButton")
        {
            if(slider.value > 0.0f)
            {
                slider.value -= 0.1f;
            }        
        } else
        {
            Debug.Log("No button found");
        }
    }
}
