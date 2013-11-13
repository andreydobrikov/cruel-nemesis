﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmasEngineModel.Management;
using Cruel.GameLogic.Unit;
using Cruel.GameLogic.Modules;
using Cruel.GameLogic.Events.UnitEvents;

namespace Cruel.GameLogic.Actions
{
    public class DealDamageToUnitAction : EntityXmasAction
	{
        public UnitEntity Target { get; private set; }
        public int Damage { get; private set; }

        public DealDamageToUnitAction(UnitEntity target, int dmg)
        {
            this.Target = target;
            this.Damage = dmg;
        }

        protected override void Execute()
        {
            HealthModule hmod = this.Target.Module<HealthModule>();
            var hp = hmod.Health;
            hp -= Damage;
            hmod.Health = hp < 0 ? 0 : hp;
            this.Source.Raise(new UnitDealsDamageEvent(Source, Target, Damage));
            var dmgtaker = Target;
            var dmgdealer = (UnitEntity)Source;
            this.Target.Raise(new UnitTakesDamageEvent(dmgtaker, dmgdealer, Damage));
        }
    }
}