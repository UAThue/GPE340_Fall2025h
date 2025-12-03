using UnityEngine;
using UnityEngine.SceneManagement;

public class PressStartUI : MonoBehaviour
{
    public void Update()
    {
        if (Input.anyKeyDown)
        {
            OnPressX();
        }
    }

    public void OnPressX()
    {
        // TODO: This is temporary! Change to load Main Menu once main menu exists!
        SceneManager.LoadScene("SampleScene");
    }
}
