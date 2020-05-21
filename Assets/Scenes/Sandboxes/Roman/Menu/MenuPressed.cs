using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPressed : BaseInteraction
{
    public GameObject menuPressed;
    public GameObject slider;
    public GameObject mainMenu;
    public string level;
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
        switch (menuPressed.name)
        {
            //Quit Button pressed
            case "QuitButton":
                Application.Quit();
                Debug.Log("Quit");
                break;
            //Options Button opens sound slider
            case "OptionsButton":
                mainMenu.SetActive(false);
                slider.SetActive(true);
                break;
            //Play Button loads the Game Scene
            case "PlayButton":
                SceneManager.LoadScene(level);
                break;
            //Back Button brings you back to the Main menu
            case "BackButton":
                slider.SetActive(false);
                mainMenu.SetActive(true);
                break;
            default:
                Debug.Log("No button found");
                break;
        }
    }
}
