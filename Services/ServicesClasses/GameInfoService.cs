﻿using System;
using Assets.SimpleLocalization;
using Master.QSpaceCode.Services.Mediator;
using Master.QSpaceCode.Services.ServicesInterfaces;
using UnityEngine;
using WebSocketSharp;

namespace Master.QSpaceCode.Services.ServicesClasses
{
    public sealed class GameInfoService : Service, IGameInfoService, IGameInfoKeeper
    {
        public GameInfoService(ServicesMediator newServicesMediator) : base(newServicesMediator)
        {
        }

        public event Action ChangeLocalizationEvent;

        public override void Init()
        {
            base.Init();
            
            LocalizationManager.Read();

            switch (Application.systemLanguage)
            {
                case SystemLanguage.Russian:
                    LocalizationManager.Language = "Russian";
                    break;
                default:
                    LocalizationManager.Language = "English";
                    break;
            }
        }

        public string GetLocalizedText(string key)
        {
            if (key.IsNullOrEmpty()) return string.Empty;
            return LocalizationManager.Localize(key);
        }
    }
}