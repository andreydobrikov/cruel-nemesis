﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cruel.GameLogic.Modules;

namespace Cruel.GameLogic.Unit
{
    public class DragonUnit : UnitEntity
    {
        public DragonUnit(Player owner) : base(owner)
        {
            this.RegisterModule(new MoveModule(5));
            this.Module<AttackModule>().AttackRange = 1;
            this.Module<AttackModule>().Damage = 5;
            this.Module<HealthModule>().MaxHealth = 5;
            this.Module<HealthModule>().Health = 5;
        }
    }
}
