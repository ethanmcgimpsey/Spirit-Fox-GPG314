using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerZAxisTracker : MonoBehaviour
{
    public TextMeshProUGUI zPositionText;
    public TextMeshProUGUI zFinalPositionText;
    public int playerZPosition;

    void Update()
    {
        // Get the player's Z-axis position
        playerZPosition = Mathf.RoundToInt(transform.position.z);

        // Convert zPosition to meters if needed
        // For example, if your Unity project is set up so that 1 unit = 1 meter,
        // then you can directly use zPosition as meters

        // Update the UI Text with the player's Z-axis position
        zPositionText.text = "Player position: " + playerZPosition + " meters";
        zFinalPositionText.text = "Player position: " + playerZPosition + " meters";
    }

    public float GetPlayerZPosition()
    {
        return playerZPosition;
    }
}
