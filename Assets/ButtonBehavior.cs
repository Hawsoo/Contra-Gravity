using UnityEngine;
using System.Collections;

public class ButtonBehavior : MonoBehaviour
{
    public static int index = 0;
    //private static bool acceptInput = true;

    public GameObject indicator;
    public int buttonIndex;
    //private int prevInput;

    public bool isMostLeft;
    public bool isMostRight;

    // Update
    void Update()
    {
        // Reset indicator
        indicator.SetActive(false);

        // If button is selected
        if (index == buttonIndex)
        {
            indicator.SetActive(true);

            // Check input
            int input = Signum(Input.GetAxisRaw("Horizontal"));

            // Accept
            if (Input.GetKeyDown(KeyCode.Return)
                || Input.GetKeyDown(KeyCode.Space))
            {
                SendMessage("ActionHappened");
            }

            // Scroll thru
            if (Input.GetKeyDown(KeyCode.LeftArrow)
                || Input.GetKeyDown(KeyCode.RightArrow))
            {
                index += input;

                if (isMostLeft && index < buttonIndex) { index = buttonIndex; }
                if (isMostRight && index > buttonIndex) { index = buttonIndex; }
            }

            //prevInput = input;
        }
    }

    private int Signum(float x)
    {
        if (x == 0)
            return 0;
        else if (x > 0)
            return 1;
        else
            return -1;
    }
}
