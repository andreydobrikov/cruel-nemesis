﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;
using Assets.GameLogic.Events;

namespace Assets.GameLogic.Actions
{
	public class StartGameAction : EnvironmentAction
	{

        protected override void Execute()
        {
            this.EventManager.Raise(new GameStartEvent());
        }
    }
}