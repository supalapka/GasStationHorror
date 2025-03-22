using TMPro;
using UnityEngine;

public class DialogueUIController : MonoBehaviour
{
    public static DialogueUIController instance;
    public TextMeshProUGUI dialogueLineText;

    private void Start()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);

        gameObject.SetActive(false);
    }

}
