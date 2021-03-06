﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using XmasEngineModel;
using XmasEngineModel.Management;
using Cruel.GameLogic.Events;
using Cruel.GameLogic.SpellSystem;
using CruelTest.SpellSystem;

namespace Assets.UnityLogic.Gui
{
	public class GUIManaViewHandler : MonoBehaviour
	{
        public UnityFactory Factory { get; set; }
        public GuiInformation GUIInfo;
        public XmasModel Engine;
        public bool reversed;
        private float manaBarHeight = 188;
        private float translateXBackground = 200;
        private float translateYBackground = 37;
        private float translateX = 200;
        private float translateY = 37;
        private float spacing = 100;
        private Dictionary<Mana, GUITexture> manaBars = new Dictionary<Mana, GUITexture>();
        private Dictionary<Mana, GUIText> manaTexts = new Dictionary<Mana, GUIText>();

        public void Initialize(UnityFactory factory, GuiInformation info, XmasModel engine, bool reversed)
        {
            this.Factory = factory;
            this.GUIInfo = info;
            this.Engine = engine;
            this.reversed = reversed;
            Engine.EventManager.Register(new Trigger<ManaCrystalAddedEvent>(evt=>evt.Owner==this.GUIInfo.Player,OnManaAdded));
            Engine.EventManager.Register(new Trigger<ManaCrystalSpentEvent>(evt => evt.Owner == this.GUIInfo.Player, OnManaSpent));
            Engine.EventManager.Register(new Trigger<ManaRechargedEvent>(evt => evt.Owner == this.GUIInfo.Player, OnManaRecharged));


        }

        private void OnManaAdded(ManaCrystalAddedEvent evt)
        {
            Mana mana = evt.CrystalType;
            if (!manaBars.ContainsKey(mana))
            {
                CreateManaBar(mana);
                translateX += spacing;
                translateXBackground += spacing;
            }
            else
                UpdateManaLevel(evt.CrystalType);
        }

        private void CreateManaBar(Mana mana)
        {
            var emptyBar = Factory.CreateEmptyManaBar();
            var position = emptyBar.transform.position;
            position.z = -1;
            emptyBar.transform.position = position;
            emptyBar.transform.parent = this.GUIInfo.Portrait.transform;
            var scaler = emptyBar.GetComponent<GUITextureAutoScaler>();
            var size = scaler.CurPlacement;
            var offsetX = reversed ? (-1 * (translateX + (size.width / 2))) : translateX;
            size.x += offsetX;
            size.y -= translateY;
            scaler.CurPlacement = size;

            var manaBar = Factory.CreateManaBar(mana);
            manaBars[mana] = manaBar;
            manaBar.transform.parent = this.GUIInfo.Portrait.transform;
            var manaScaler = manaBar.GetComponent<GUITextureAutoScaler>();
            var manaSize = manaScaler.CurPlacement;
            var manaOffsetX = reversed ? (-1 * (translateXBackground + (manaSize.width / 2))) : translateX;
            manaSize.x += manaOffsetX;
            manaSize.y -= translateYBackground;
            manaScaler.CurPlacement = manaSize;
            var manaText = Factory.CreateText();
            manaText.color = Color.white;
            manaText.transform.parent = this.GUIInfo.Portrait.transform;
            manaText.GetComponent<GUITextureAutoScaler>().CurPlacement = manaScaler.CurPlacement;
            manaTexts.Add(mana, manaText);
            UpdateMana();
        }

        private void OnManaSpent(ManaCrystalSpentEvent evt)
        {
            UpdateManaLevel(evt.CrystalType);
        }

        private void UpdateMana()
        {
            foreach (Mana m in this.GUIInfo.Player.ManaStorage.getAllTypes())
                UpdateManaLevel(m);
        }

        private void OnManaRecharged(ManaRechargedEvent evt)
        {
            UpdateMana();
        }

        private void UpdateManaLevel(Mana mana)
        {
            ManaStorage manaStorage = this.GUIInfo.Player.ManaStorage;
            float maxVal = manaStorage.Size(mana);
            float curVal = manaStorage.GetChargedCount(mana);
            var manaBar = manaBars[mana];
            var scaler = manaBar.GetComponent<GUITextureAutoScaler>();
            var size = scaler.CurPlacement;
            float newHeight = (manaBarHeight * curVal) / maxVal;
            var difference = size.height - newHeight;
            var newSize = new Rect(size.xMin, size.yMin+difference, size.width, newHeight);
            scaler.CurPlacement = newSize;
            this.manaTexts[mana].text = curVal + " / " + maxVal;
        }
	}
}
