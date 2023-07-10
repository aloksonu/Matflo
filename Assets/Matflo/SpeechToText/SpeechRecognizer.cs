using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpeechRecognizer  : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    void Start()
    {
        Debug.Log("Start");
        actions.Add("forward", Forward);
        actions.Add("backward", Backward);
        actions.Add("up", Up);
        actions.Add("down", Down);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += ReconizedSpeech;
        keywordRecognizer.Start();
    }

    private void ReconizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();

    }

    private void Forward()
    {
        Debug.Log("Forward");
    }
    private void Backward()
    {
        Debug.Log("Backward");
    }
    private void Up()
    {
        Debug.Log("Up");
    }
    private void Down()
    {
        Debug.Log("Down");
    }
}
