﻿using Master.QSpaceCode.Configs;
using Master.QSpaceCode.Services.Mediator;
using UnityEngine;

namespace Master.QSpaceCode
{
    public sealed class Core : MonoBehaviour
    {
        [SerializeField] private ScenesConfig scenesConfig;
        [SerializeField] private AudioConfig audioConfig;

        public static ScenesConfig ScenesConfig => singleton.scenesConfig;
        public static AudioConfig AudioConfig => singleton.audioConfig;

        private static readonly ServicesMediator ServicesMediator = new ServicesMediator();

        public static IGameInfoKeeper GameInfoKeeper => ServicesMediator.gameInfoKeeper;
        public static IPunInfoKeeper PunInfoKeeper => ServicesMediator.punInfoKeeper;
        public static IUiStateKeeper UiStateKeeper => ServicesMediator.uiStateKeeper;
        public static IUiInputKeeper UiInputKeeper => ServicesMediator.uiInputKeeper;
        public static IViewersKeeper ViewersKeeper => ServicesMediator.viewersKeeper;
        public static ISoundsKeeper SoundsKeeper => ServicesMediator.soundsKeeper;

        private static Core singleton;

        public static UiHelper UIHelper { get; } = new UiHelper();

        private void Awake()
        {
            if (!singleton)
            {
                singleton = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                DestroyImmediate(gameObject);
            }
        }

        private void Start()
        {
            ServicesMediator.Init();
        }

        private void Update()
        {
            ServicesMediator.Runtime();
            UIHelper.UpdateHelper();
        }
    }
}