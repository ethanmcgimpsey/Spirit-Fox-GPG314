using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using MalbersAnimations;
using MalbersAnimations.Controller;
using UnityEditor.Experimental.GraphView;
using Unity.VisualScripting.Dependencies.Sqlite;

public class NPCDialogue : MonoBehaviour
{
    public MInput mInput;
    public MAnimal mAnimal;

    public Animator afterDialogue;

    // Array to hold the NPC's dialogue lines
    public string[] dialogueLines;

    // Index to keep track of the current line of dialogue
    [SerializeField] private int currentLineIndex = 0;

    // Boolean to check if the NPC dialogue is active
    [SerializeField] private bool isDialogueActive = false;

    // Reference to TextMeshPro UI component on the Canvas
    public TextMeshProUGUI dialogueText;

    // Reference to the player GameObject
    public GameObject player;
    public GameObject dialogueBox;

    // The distance threshold for starting the dialogue
    public float dialogueDistance = 3f;

    // Should player be able to interact with npc again?
    [SerializeField] private bool interactNPC;
    [SerializeField] private bool oneTimeInteractNPC;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure the TextMeshPro component is assigned
        if (dialogueText == null)
        {
            Debug.LogError("Please assign a TextMeshPro component to the NPCDialogue script in the Unity Inspector.");
        }
        // Initially hide the dialogue text
        dialogueBox.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
         // Check for user input to start the dialogue
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartDialogue();
        }

        // Check for user input to continue dialogue or end it
        if (isDialogueActive && Input.GetMouseButtonDown(0))
        {
            DisplayNextLine();
        }
    }

    void PlayAnimation()
    {
        if(afterDialogue != null)
        {
            afterDialogue.Play("DreamIntro");
        }
    }

    // Function to start the NPC dialogue
    public void StartDialogue()
    {
        if (interactNPC)
        {
            if (!isDialogueActive && Vector3.Distance(transform.position, player.transform.position) <= dialogueDistance)
            {
                mInput.DisableInput("Jump");
                mAnimal.LockMovement = true;
                // Set dialogue to active
                isDialogueActive = true;

                // Reset current line index
                currentLineIndex = 0;

                // Display the first line of dialogue
                DisplayNextLine();

                // Show the dialogue text
                dialogueBox.gameObject.SetActive(true);
            }
        }
    }

    // Function to display the next line of dialogue
    private void DisplayNextLine()
    {
        if (currentLineIndex < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[currentLineIndex];
            currentLineIndex++;
        }
        else
        {
            // End dialogue if there are no more lines
            EndDialogue();
            PlayAnimation();
        }
    }

    // Function to end the NPC dialogue
    public void EndDialogue()
    {
        if (oneTimeInteractNPC)
        {
            interactNPC = false;
        }

        // Reset current line index
        currentLineIndex = 0;

        // Set dialogue to inactive
        isDialogueActive = false;

        // Clear the dialogue text
        dialogueText.text = "";

        // Hide the dialogue text
        dialogueBox.gameObject.SetActive(false);

        // Re-enable jumping and movement
        mInput.EnableInput("Jump");
        mAnimal.LockMovement = false;

        // Debug message indicating end of dialogue
        Debug.Log("End of dialogue");
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, dialogueDistance);
    }
}
