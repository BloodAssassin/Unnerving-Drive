using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    [System.Serializable]

    public struct Line
    {
        public string name;
        [TextArea(3, 10)]
        public string sentence;
        public Sprite character;
        public Vector2 position;
        public int rotation;
    }
    [System.Serializable]
    public struct Dialogue
    {
        public List<Line> lines;
    }

    [SerializeField] CanvasGroup canvasOpacity;
    [SerializeField] GameObject portrait;
    [HideInInspector] public Sprite backgroundSprite;
    [SerializeField] TextMeshProUGUI textField;
    [SerializeField] GameObject indicator;
    [SerializeField] List<Dialogue> dialogue;
    private int count;
    public int index;

    private bool sentenceFinished = false;
    private bool skipSentence = false;
    private bool inDialogue = false;

    void Start()
    {
        textField.text = "";
        indicator.SetActive(false);
        canvasOpacity.alpha = 0f;
        portrait.GetComponent<CanvasGroup>().alpha = 0f;

        StartDialogue();
    }

    void Update()
    {
        Interact();
    }

    private void Interact()
    {
        if (inDialogue == false)
        {
            return;
        }

        // Click
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            //SKIP SENTENCE
            if (sentenceFinished == false && indicator.activeInHierarchy == false && textField.text != "")
            {
                textField.text = dialogue[index].lines[count].sentence;
                skipSentence = true;
            }
            //NEXT SENTENCE
            else if (sentenceFinished == true && dialogue[index].lines.Count > count + 1)
            {
                sentenceFinished = false;
                NextSentence();
                count++;
            }
            //END DIALOGUE
            else if (sentenceFinished == true && dialogue[index].lines.Count == count + 1)
            {
                EndDialogue();
            }
        }

        //IF SENTENCE IS COMPLETE - CANCEL SKIP
        if (skipSentence == true && indicator.activeInHierarchy == true)
        {
            skipSentence = false;
        }
    }

    // Dialogue Interface
    public void StartDialogue()
    {
        LeanTween.value(gameObject, 0f, 1f, 0.5f).setOnUpdate((float val) =>
        {
            canvasOpacity.alpha = val;
        });

        SceneLoader.gamePaused = true;
        inDialogue = true;
        FirstSentence();
    }

    private void EndDialogue()
    {
        textField.text = "";
        inDialogue = false;
        indicator.SetActive(false);
        SceneLoader.gamePaused = false;
        portrait.GetComponent<CanvasGroup>().alpha = 0f;

        LeanTween.value(gameObject, 1f, 0f, 0.5f).setOnUpdate(val => canvasOpacity.alpha = val);
        this.gameObject.SetActive(false);
        GameObject.Find("ObjectSpawner").GetComponent<ObjectSpawner>().enabled = true;
    }

    private void FirstSentence()
    {
        LeanTween.value(gameObject, 0f, 1f, 0.5f).setOnComplete(() =>
        {
            indicator.SetActive(false);
            portrait.GetComponent<Image>().sprite = dialogue[index].lines[count].character;
            portrait.transform.localPosition = dialogue[index].lines[count].position;
            portrait.transform.localScale = new Vector2(1 * dialogue[index].lines[count].rotation, 1);

            LeanTween.value(gameObject, 0f, 1f, 0.5f).setOnUpdate((float val) =>
            {
                portrait.GetComponent<CanvasGroup>().alpha = val;
                if (val == 1f)
                {
                    StartCoroutine(TypeSentence(dialogue[index].lines[count]));
                }
            });
        });
    }

    private void NextSentence()
    {
        indicator.SetActive(false);
        textField.text = "";

        if (portrait.GetComponent<Image>().sprite != dialogue[index].lines[count + 1].character ||
            portrait.transform.localPosition.x != dialogue[index].lines[count + 1].position.x ||
            portrait.transform.localScale.x != dialogue[index].lines[count + 1].rotation)
        {
            LeanTween.value(gameObject, 1f, 0f, 0.2f).setOnUpdate((float val) =>
            {
                portrait.GetComponent<CanvasGroup>().alpha = val;
                if (val == 0f)
                {
                    portrait.GetComponent<Image>().sprite = dialogue[index].lines[count].character;
                    portrait.transform.localPosition = dialogue[index].lines[count].position;
                    portrait.transform.localScale = new Vector2(1 * dialogue[index].lines[count].rotation, 1);
                    LeanTween.value(gameObject, 0f, 1f, 0.2f).setOnUpdate((float val) =>
                    {
                        portrait.GetComponent<CanvasGroup>().alpha = val;
                        if (val == 1f)
                        {
                            StartCoroutine(TypeSentence(dialogue[index].lines[count]));
                        }
                    });
                }
            });
        }
        else
        {
            portrait.GetComponent<Image>().sprite = dialogue[index].lines[count + 1].character;
            portrait.transform.localPosition = dialogue[index].lines[count + 1].position;
            portrait.transform.localScale = new Vector2(1 * dialogue[index].lines[count + 1].rotation, 1);
            StartCoroutine(TypeSentence(dialogue[index].lines[count + 1]));
        }
    }

    private IEnumerator TypeSentence(Line line)
    {
        textField.text = "";
        foreach (char letter in line.sentence)
        {
            if (skipSentence == true)
            {
                textField.text = line.sentence;
                skipSentence = false;
                break;
            }

            textField.text += letter;
            yield return new WaitForSecondsRealtime(0.03f);
        }

        indicator.SetActive(true);
        sentenceFinished = true;
    }
}
