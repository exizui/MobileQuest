using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using UnityEngine;

//public class DialogueSwitcher : MonoBehaviour
//{
//    [SerializeField] private string[] _disableTags;
//    private DialogueStory _dialogueStory;


//    private void Start()
//    {
//        _dialogueStory = FindObjectOfType<DialogueStory>();
//        _dialogueStory.ChangedStory += Disable;
//    }

//    private async void Disable(DialogueStory.Story story)
//    {
//        if (_disableTags.All(disableTag => story.Tag != disableTag)) return;
//        await Task.Delay(1000);
//        _dialogueStory.gameObject.SetActive(false);
//    }
//}
