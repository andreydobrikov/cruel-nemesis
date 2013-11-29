﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using XmasEngineModel;
using Cruel.GameLogic.SpellSystem;
using Cruel.Map.Terrain;
using XmasEngineExtensions.TileExtension;
using Assets.UnityLogic.Gui;
using JSLibrary.Data;
using Assets.UnityLogic.Unit;
using Cruel.GameLogic.Unit;

namespace Assets.UnityLogic
{
	public class UnityFactory : MonoBehaviour
	{

        public EngineHandler Engine;
        public Transform CardTemplate;
        public Transform TerrainTemplate;
        public Transform UnitTemplate;
        public Transform HealthBarTemplate;

        private Dictionary<object, GameObject> gameobjLookUp = new Dictionary<object, GameObject>();

        public object GameObjectFromModel(object modelobj)
        {
            GameObject gobj = null;
            gameobjLookUp.TryGetValue(modelobj, out gobj);
            return gobj;
        }

        public Transform CreateTile(TerrainEntity terEnt, TilePosition posinfo)
        {
            var pos = posinfo.Point;
            var tileobj = (Transform)Instantiate(TerrainTemplate, new Vector3(-(float)pos.X, (float)pos.Y), TerrainTemplate.rotation);
            gameobjLookUp.Add(terEnt, tileobj.gameObject);
            return tileobj;
        }

        public Transform CreateCard(GameCard gameCard)
        {
            var cardobj = (Transform)GameObject.Instantiate(CardTemplate);
            var cardinfo = cardobj.gameObject.AddComponent<CardInformation>();
            cardinfo.Card = gameCard;
            gameobjLookUp.Add(gameCard, cardobj.gameObject);
            return cardobj;
        }

        public  Vector3 ConvertUnitPos(Point pos)
        {
            return new Vector3(-(float)pos.X, (float)pos.Y + 0.5f, 0.3f);
        }

        public Transform CreateUnit(UnitEntity unitEnt, TilePosition posinfo)
        {
            var pos = posinfo.Point;
            Quaternion ur = UnitTemplate.rotation;
            Quaternion rot = new Quaternion(ur.x + 0.14f, ur.y, ur.z, ur.w);
            Vector3 unitvec = ConvertUnitPos(pos);
            var unitobj = (Transform)Instantiate(UnitTemplate, unitvec, rot);
            var info = unitobj.gameObject.AddComponent<UnitInformation>();

            unitobj.gameObject.AddComponent<UnitViewHandler>();
            unitobj.gameObject.AddComponent<UnitControllerHandler>();

            info.SetEntity(unitEnt);
            UnitGraphics graphic = UnitGraphicFactory.ConstuctUnitGraphic(unitEnt.getUnitType());
            info.SetGraphics(graphic);

            var viewhandler = unitobj.gameObject.GetComponent<UnitViewHandler>();
            viewhandler.HealthBar = (Transform)Instantiate(this.HealthBarTemplate);
            viewhandler.HealthBar.GetComponent<HealthbarView>().SetPosition(pos);
            this.gameobjLookUp.Add(unitEnt,unitobj.gameObject);
            return unitobj;
        }
    }
}