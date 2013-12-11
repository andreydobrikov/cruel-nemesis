﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.UnityLogic.Unit
{
	public abstract class UnitGraphic
	{
        private Dictionary<StandardUnitAnimations, TextureAnimation> animations = new Dictionary<StandardUnitAnimations, TextureAnimation>();

     

        public abstract void LoadAnimations();

        protected void SetAnimation(StandardUnitAnimations aniId, TextureAnimation ani)
        {
            this.animations[aniId] = ani;
        }

        public bool HasAnimation(StandardUnitAnimations ani)
        {
            return animations.ContainsKey(ani);
        }


        public TextureAnimation GetAnimation(StandardUnitAnimations ani)
        {
            return animations[ani];
        }

        public TextureAnimation[] Animations 
        {
            get
            {
                return animations.Values.ToArray();
            }
            
        }
    }
}
