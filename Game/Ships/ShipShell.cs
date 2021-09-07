using System.Collections.Generic;
using Master.QSpaceCode.Configs.ShipsConfigs;
using UnityEngine;

namespace Master.QSpaceCode.Game.Ships
{
    public abstract class ShipShell : MonoBehaviour
    {
        [Space] [SerializeField] private Transform[] marchJets;
        [SerializeField] private Transform[] leftJets;
        [SerializeField] private Transform[] rightJets;
        [SerializeField] private Transform[] forwardJets;

        public float BaseHealth => shipShellConfig.BaseHealth;
        public float MarchSpeed => shipShellConfig.MarchSpeed;
        public float MoveSpeed => shipShellConfig.MoveSpeed;
        public float RotateSpeed => shipShellConfig.RotateSpeed;
        public float MarchPowerSpend => shipShellConfig.MarchPowerSpend;
        public float MovePowerSpend => shipShellConfig.MovePowerSpend;

        public float RotatePowerSpend => shipShellConfig.RotatePowerSpend;

        private readonly Dictionary<Transform, Vector3> jetsStartScale = 
            new Dictionary<Transform, Vector3>();

        private ShipShellConfig shipShellConfig;

        public virtual void LoadConfig(ShipShellConfig newConfig)
        {
            shipShellConfig = newConfig;
            foreach (var jet in marchJets) jetsStartScale.Add(jet, jet.localScale);
            foreach (var jet in leftJets) jetsStartScale.Add(jet, jet.localScale);
            foreach (var jet in rightJets) jetsStartScale.Add(jet, jet.localScale);
            foreach (var jet in forwardJets) jetsStartScale.Add(jet, jet.localScale);
        }

        public virtual void UpdateJets(Vector2 moveVector)
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