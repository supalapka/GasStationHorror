using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueSystem : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public GameObject dialoguePanel;
    public float typingSpeed = 0.05f;
    public string[] dialogueLines;
    private int currentLineIndex = 0;
    private bool isTyping = false;

    private bool canDialogue = false;
    private bool isNpcInPosition = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<DialogueActivator>(out DialogueActivator _dialogueActivator))
            isNpcInPosition = true;


        if (other.CompareTag("Player") && isNpcInPosition)
        {
            canDialogue = true;
            ShowNextDialogue();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<DialogueActivator>(out DialogueActivator _dialogueActivator))
            isNpcInPosition = false;

        if (other.CompareTag("Player"))
        {
            canDialogue = false;
            dialoguePanel.SetActive(false);
        }
    }

    private void Update()
    {
        if (canDialogue)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                if (isTyping)
                {
                    // Если текст еще печатается, мгновенно показать его
                    StopAllCoroutines();
                    dialogueText.text = dialogueLines[currentLineIndex - 1];
                    isTyping = false;
                }
                else
                {
                    // Иначе перейти к следующей строке
                    ShowNextDialogue();
                }
            }
        }

    }

    private void ShowNextDialogue()
    {
        dialoguePanel.SetActive(true);
        if (currentLineIndex < dialogueLines.Length)
        {
            StartCoroutine(TypeText(dialogueLines[currentLineIndex]));
            currentLineIndex++;
        }
        else
        {
            Debug.Log("Dialogue is ended");
            dialoguePanel.SetActive(false);
        }
    }

    private IEnumerator TypeText(string line)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char letter in line)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }
}
