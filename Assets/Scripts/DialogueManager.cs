using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    Animator anim;
    [SerializeField]
    private Queue<string> sentences;
    [SerializeField]
    private Queue<string> names;
    [SerializeField]
    private Queue<Sprite> faces;
    [SerializeField]
    private Text nameText;
    [SerializeField]
    private Text dialogueText;
    [SerializeField]
    private Image spriteFace;

    void Start()
    {
        sentences = new Queue<string>();
        names = new Queue<string>();
        faces = new Queue<Sprite>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        SceneSave.sceneSave.inDialogue = true;

        SceneSave.sceneSave.ChangeCanMove(false);
        anim.SetBool("IsOpen", true);
        sentences.Clear();

        foreach (string name in dialogue.names)
        {
            names.Enqueue(name);
        }
        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        foreach (Sprite face in dialogue.faces)
        {
            faces.Enqueue(face);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        string name = names.Dequeue();
        Sprite sprite = faces.Dequeue();

        nameText.text = name;
        spriteFace.sprite = sprite;
        spriteFace.SetNativeSize();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            SoundManager.soundManager.PlayDialogue();
            yield return new WaitForSeconds(0.01f);
        }
    }

    void EndDialogue()
    {
        SceneSave.sceneSave.ChangeCanMove(true);
        anim.SetBool("IsOpen", false);
        SceneSave.sceneSave.inDialogue = false;

    }
}
