using System.Collections.Generic;
using Master.QSpaceCode.Configs;
using Master.QSpaceCode.Configs.Ships;
using UnityEngine;

namespace Master.QSpaceCode.Game.Ships
{
    public abstract class ShipShell : MonoBehaviour
    {
        [Space] [SerializeField] private Transform[] marchJets;
        [SerializeField] private Transform[] forwardJets;
        [SerializeField] private Transform[] jets1;
        [SerializeField] private Transform[] jets2;
        [SerializeField] private Transform[] jets3;
        [SerializeField] private Transform[] jets4;

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
            foreach (var jet in forwardJets) jetsStartScale.Add(jet, jet.localScale);
            foreach (var jet in jets1) jetsStartScale.Add(jet, jet.localScale);
            foreach (var jet in jets2) jetsStartScale.Add(jet, jet.localScale);
            foreach (var jet in jets3) jetsStartScale.Add(jet, jet.localScale);
            foreach (var jet in jets4) jetsStartScale.Add(jet, jet.localScale);
        }

        public void UpdateJets(Vector2 moveVector, float rotation)
        {
            if (moveVector.x > 0)
            {
                WorkWithJets(jets1, moveVector.x);
                WorkWithJets(jets2, moveVector.x);
                WorkWithJets(jets3, 0);
                WorkWithJets(jets4, 0);
            }
            else
            {
                WorkWithJets(jets1, 0);
                WorkWithJets(jets2, 0);
                WorkWithJets(jets3, Mathf.Abs(moveVector.x));
                WorkWithJets(jets4, Mathf.Abs(moveVector.x));
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

            if (rotation > 0)
            {
                if (rotation > moveVector.x) WorkWithJets(jets2, rotation);
                if (-rotation < moveVector.x) WorkWithJets(jets4, rotation);
            }
            else if (rotation < 0)
            {
                if (rotation < moveVector.x) WorkWithJets(jets3, -rotation);
                if (-rotation > moveVector.x) WorkWithJets(jets1, -rotation);
            }
        }

        private void WorkWithJets(Transform[] jets, float size)
        {
            foreach (var jet in jets) jet.localScale = jetsStartScale[jet] * size;
        }
    }
}