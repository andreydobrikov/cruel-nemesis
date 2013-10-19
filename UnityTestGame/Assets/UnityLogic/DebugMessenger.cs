﻿using UnityEngine;
using System.Collections;
using XmasEngineModel;
using XmasEngineModel.Management;
using Assets.GameLogic.Events;
using XmasEngineModel.Management.Events;
using Assets.GameLogic.Actions;
using Assets.GameLogic.PlayerCommands;

public class DebugMessenger : MonoBehaviour {

    private XmasModel engmodel;
    public EngineHandler Engine;

	// Use this for initialization
	void Start () 
    {
        engmodel = Engine.EngineModel;
        engmodel.EventManager.Register(new Trigger<ActionFailedEvent>(evt => Debug.Log("Engine action("+evt.FailedAction+") failed: " + evt.Exception.Message+" at "+evt.Exception.StackTrace)));
        engmodel.EventManager.Register(new Trigger<EndMoveEvent>(evt => Debug.Log("Unit has moved to " + evt.To)));
        engmodel.EventManager.Register(new Trigger<PlayersTurnChangedEvent>(evt => Debug.Log("Player turn changed to: "+evt.PlayersTurn.Name)));
        engmodel.EventManager.Register(new Trigger<PhaseChangedEvent>(evt => Debug.Log("Phase changed to: " + evt.NewPhase.ToString())));
        engmodel.EventManager.Register(new Trigger<PlayerGainedPriorityEvent>(evt => { if (evt.Player != null) Debug.Log("Player gained priority: " + evt.Player.Name); }));
        engmodel.EventManager.Register(new Trigger<PlayerJoinedEvent>(evt => Debug.Log("Player joined: " + evt.Player.Name )));
        engmodel.EventManager.Register(new Trigger<TriggerFailedEvent>(evt => Debug.Log(evt.FailedTrigger + " failed: " + evt.Exception.Message + " at " + evt.Exception.StackTrace)));
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
