﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace Dialogue
{
    public class DialogueNode : ScriptableObject
    {
        [SerializeField] private bool isPlayerSpeaking = false;
        [SerializeField] private bool isTextPaused = true;
        
        [SerializeField] [TextArea(15, 20)] private string text;
        [SerializeField] private List<string> children = new List<string>();
	    [SerializeField] private Rect rect = new Rect(0, 0, 200, 120);
	    
	    [SerializeField] private string onEnterAction;
	    [SerializeField] private string onExitAction;

        public Rect GetRect() 
        { 
            return rect; 
        }
        public string GetText()
        {
            return text;
        }
        public List<string> GetChildren()
        {
            return children;
        }
        public bool IsPlayerSpeaking()
        {
            return isPlayerSpeaking;
        }
        public bool IsTextPaused()
        {
            return isTextPaused;
        }
	    public string GetOnEnterAction()
	    {
	    	return onEnterAction;
	    }
	    public string GetOnExitAction()
	    {
	    	return onExitAction;
	    }
        
        
#if UNITY_EDITOR
        public void SetPosition(Vector2 newPosition)
        {
            Undo.RecordObject(this, "Move Dialogue Node");
            rect.position = newPosition;
            EditorUtility.SetDirty(this);
        }
        public void SetText(string newText)
        {
            if (newText != text)
            {
                Undo.RecordObject(this, "Update Dialogue Text");
                text = newText;
                EditorUtility.SetDirty(this);
            }
        }
        public void AddChild(string childID)
        {
            Undo.RecordObject(this, "Add Dialogue Link");
            children.Add(childID);
            EditorUtility.SetDirty(this);
        }
        public void RemoveChild(string childID)
        {
            Undo.RecordObject(this, "Remove Dialogue Link");
            children.Remove(childID);
            EditorUtility.SetDirty(this);
        }
        public void SetPlayerSpeaking(bool newIsPlayerSpeaking)
        {
            Undo.RecordObject(this, "Change Dialogue Speaker");
            isPlayerSpeaking = newIsPlayerSpeaking;
            EditorUtility.SetDirty(this);
        }
        public void SetPauseText(bool pause)
        {
            Undo.RecordObject(this, "Pause Dialogue");
            isTextPaused = pause;
            EditorUtility.SetDirty(this);
        }
#endif
    }   
}