using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionIndicator : MonoBehaviour
{

    private string interactButton = "E";
    [SerializeField] private TextMeshProUGUI interactionActionText;
    [SerializeField] private TextMeshProUGUI interactionNameText;
    [SerializeField] private TextMeshProUGUI interactionDescriptionText;
    private string interactionAction;
    private string interactionName;
    private string interactionDescription;

    private void Start()
    {
        ResetText();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Interactable")
        {
            var interactInfo = collision.gameObject.GetComponent<InteractObject>();

            interactionActionText.gameObject.SetActive(true);

            interactionAction = "[" + interactButton + "] - " + interactInfo.GetInteractAction();
            interactionName = interactInfo.GetInteractName();
            interactionDescription = interactInfo.GetInteractDesc();

            interactionActionText.text = interactionAction;
            interactionNameText.text = interactionName;
            interactionDescriptionText.text = interactionDescription;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Interactable")
        {
            interactionActionText.gameObject.SetActive(false);
            ResetText();
        }
    }

    private void ResetText()
    {
        interactionAction = "";
        interactionName = "";
        interactionDescription = "";
        interactionActionText.text = interactionAction;
        interactionNameText.text = interactionName;
        interactionDescriptionText.text = interactionDescription;
    }

}
