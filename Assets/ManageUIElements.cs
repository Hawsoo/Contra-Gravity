using UnityEngine;
using System.Collections;

public class ManageUIElements : MonoBehaviour
{
    public GameObject obj1;
    public GameObject obj2;
    public GameObject obj3;

    // Update
    void FixedUpdate()
    {
        // Cycle thru, disabling every object
        for (int i = 1; i <= 3; i++)
        {
            ChangeObject(i, false);
        }
    }

    private void ChangeObject(int id, bool enable)
    {
        // Change certain object
        switch (id)
        {
            case 1:
                if (obj1 != null)
                    obj1.SetActive(enable);
                break;
            case 2:
                if (obj2 != null)
                    obj2.SetActive(enable);
                break;
            case 3:
                if (obj3 != null)
                    obj3.SetActive(enable);
                break;
        }
    }

    // Messages
    void ShowActionIcon(int id) { ChangeObject(id, true); }
}
