﻿using System.Collections.Generic;
using Master.QSpaceCode.Configs;
using Master.QSpaceCode.Configs.Ships;
using UnityEngine;

namespace Master.QSpaceCode.Game.Ships
{
    public abstract class ShipShell : MonoBehaviour
    {
        [Space] [SerializeField] private Transform[] marchJets;
        [SerializeField] private Transform[] leftJets;
        [SerializeField] private Transform[] rightJets;
        [SerializeField] private Transform[] forwardJets;

        private readonly Dictionary<Transform, Vector3> jetsStartScale = new Dictionary<Transform, Vector3>();
        
        private ShipShellConfig shipShellConfig;

        public void GetMoveSpeed(int characteristic, out float speed, out float powerSpend)
        {
            var config = CurrentConfigs.ShipsConfig;
            var mod = config .ChangeSpeedPerStep * (characteristic - 6);
            speed = (config .BaseSpeed + mod) * shipShellConfig.MoveSpeedMod;
            powerSpend = config .BaseMoveCost * shipShellConfig.MovePowerSpendMod;
        }

        public void GetMarchSpeed(int characteristic, out float speed, out float powerSpend)
        {
            var config = CurrentConfigs.ShipsConfig;
            var mod = config.ChangeSpeedPerStep * (characteristic - 6);
            speed = (config.BaseSpeed + mod) * shipShellConfig.MarchSpeedMod;
            powerSpend = config.BaseMoveCost * shipShellConfig.MarchPowerSpendMod;
        }

        public void GetRotateSpeed(int characteristic, out float speed, out float powerSpend)
        {
            var config = CurrentConfigs.ShipsConfig;
            var mod = config.ChangeSpeedPerStep * (characteristic - 6);
            speed = (config.BaseSpeed + mod) * shipShellConfig.RotateSpeedMod;
            powerSpend = config.BaseMoveCost * shipShellConfig.RotatePowerSpendMod;
        }
        
        public virtual void LoadConfig(ShipShellConfig newConfig)
        {
            shipShellConfig = newConfig;
            jetsStartScale.Clear();
            foreach (var jet in marchJets) jetsStartScale.Add(jet, jet.localScale);
            foreach (var jet in leftJets) jetsStartScale.Add(jet, jet.localScale);
            foreach (var jet in rightJets) jetsStartScale.Add(jet, jet.localScale);
            foreach (var jet in forwardJets) jetsStartScale.Add(jet, jet.localScale);
        }

        public void UpdateJets(Vector2 moveVector)
        {
            if (moveVector.x > 0)
            {
                WorkWithJets(leftJets, moveVector.x);
                WorkWithJets(rightJets, 0);
            }
            else
            {
                WorkWithJets(rightJets, Mathf.Abs(moveVector.x));
                WorkWithJets(leftJets, 0);
            }

            if (moveVector.y > 0)
            {
                WorkWithJets(marchJets, moveVector.y);
                WorkWithJets(forwardJets, 0);
            }
            else
            {
                WorkWithJets(forwardJets, Mathf.Abs(moveVector.y));
                WorkWithJets(marchJets, 0);
            }
        }

        private void WorkWithJets(Transform[] jets, float size)
        {
            foreach (var jet in jets) jet.localScale = jetsStartScale[jet] * size;
        }
    }
}