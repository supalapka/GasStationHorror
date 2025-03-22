using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Localization;

public class DialogueSystem : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public GameObject dialoguePanel;
    public float typingSpeed = 0.05f;
    public FuelTank fuelTank;

    public string dialogueKey;
    public int dialogueLinesAmount;
    public int requestFuelAfterLine;

    public Action OnDialogueCompleted;

    private int currentLineIndex = 0;
    private bool isTyping = false;
    private string currentText;

    private bool canDialogue = false;
    private bool isNpcInPosition = false;
    private bool waitingForFuel = false;


    private void OnEnable() => fuelTank.OnFuelingComplete += OnFuelCompleted;
    private void OnFuelCompleted() => waitingForFuel = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<DialogueActivator>(out DialogueActivator _dialogueActivator))
            isNpcInPosition = true;


        if (other.CompareTag("Player") && isNpcInPosition)
        {
            canDialogue = true;

            if (waitingForFuel)
                StartCoroutine(TypeText(currentText));
            else
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
                    // ≈сли текст еще печатаетс€, мгновенно показать его
                    StopAllCoroutines();
                    dialogueText.text = currentText;
                    isTyping = false;
                }
                else
                {
                    // »наче перейти к следующей строке если не ждет пока игрок заправит машину
                    if (waitingForFuel)
                        StartCoroutine(TypeText(currentText));
                    else
                        ShowNextDialogue();
                }
            }
        }

    }

    private void ShowNextDialogue()
    {
        string key = $"{dialogueKey}_{currentLineIndex}";
        LocalizedString localizedString = new LocalizedString
        {
            TableReference = "Dialogues Table",
            TableEntryReference = key
        };

        currentText = localizedString.GetLocalizedString();

        dialoguePanel.SetActive(true);
        if (currentLineIndex < dialogueLinesAmount)
        {
            StartCoroutine(TypeText(currentText));

            if (currentLineIndex == requestFuelAfterLine)
                waitingForFuel = true;

            currentLineIndex++;
        }
        else
        {
            Debug.Log("Dialogue is ended");
            dialoguePanel.SetActive(false);
            OnDialogueCompleted?.Invoke();
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
