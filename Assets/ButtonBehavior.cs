using UnityEngine;
using System.Collections;

public class ButtonBehavior : MonoBehaviour
{
    public static int index = 0;
    private static bool acceptInput = true;

    public int buttonIndex;
    private int prevInput;

    // Update
    void Update()
    {
        if (index == buttonIndex)
        {
            // Check input
            int input = Signum(Input.GetAxisRaw("Horizontal"));
            acceptInput = (input != prevInput);
            Debug.Log(acceptInput);

            // Accept
            if (Input.GetButtonDown("Fire1"))
            {
                SendMessage("ActionHappened");
            }

            // Scroll thru
            if (input != 0 && acceptInput)
            {
                index += input;
            }

            prevInput = input;
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
