using TMPro;
using UnityEngine;

public class DialogueUIController : MonoBehaviour
{
    public static DialogueUIController instance;
    public TextMeshProUGUI dialogueLineText;
    public AudioSource audioSource;


    private void Start()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);

        gameObject.SetActive(false);
    }

    public void EnableTextGeneratingSound() => audioSource.enabled = true;
    public void DisableTextGeneratingSound() { audioSource.enabled = false; }
}
