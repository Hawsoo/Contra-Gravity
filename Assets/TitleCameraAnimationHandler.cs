using UnityEngine;
using System.Collections;

public class TitleCameraAnimationHandler : MonoBehaviour
{
    public GameObject titleLogo;

	// Start new animation
    void LogoTurnStart()
    {
        titleLogo.SetActive(true);
    }
}