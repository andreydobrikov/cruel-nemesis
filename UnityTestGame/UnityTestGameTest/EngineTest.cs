﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel;
using XmasEngineModel.Management;
using UnityTestGameTest.TestComponents;

namespace UnityTestGameTest
{
    public class EngineTest : XmasActor
    {
        public XmasModel Engine { get; private set; }

        public EngineTest() : this(new MockWorld())
        {
            
        }


        public EngineTest(XmasWorld world)
        {
            this.EventManager = new EventManager();
            this.ActionManager = new ActionManager(this.EventManager);
            this.Factory = new XmasFactory(this.ActionManager);
            this.World = world;
            this.Engine = new XmasModel(this.World, this.ActionManager, this.EventManager, this.Factory);
        }
    }
}