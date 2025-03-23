using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Localization;

public class DialogueSystem : MonoBehaviour
{
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
            DialogueUIController.instance.gameObject.SetActive(false);
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
                    DialogueUIController.instance.dialogueLineText.text = currentText;
                    DialogueUIController.instance.DisableTextGeneratingSound();
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

        DialogueUIController.instance.gameObject.SetActive(true);
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
            DialogueUIController.instance.gameObject.SetActive(false);
            OnDialogueCompleted?.Invoke();
        }
    }

    private IEnumerator TypeText(string line)
    {
        isTyping = true;
        DialogueUIController.instance.EnableTextGeneratingSound();
        DialogueUIController.instance.dialogueLineText.text = "";
        foreach (char letter in line)
        {
            DialogueUIController.instance.dialogueLineText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
        DialogueUIController.instance.DisableTextGeneratingSound();
    }
}
