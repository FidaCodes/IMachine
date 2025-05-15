using System.Collections;
using System.Collections.Generic;
using Unity.FPS.Game;
using UnityEngine;

namespace Unity.FPS.Gameplay
{
    public class ObjectiveActivateSwitches : Objective
    {
        [Tooltip("Choose whether you need to activate all switches or only a minimum amount")]
        public bool MustActivateAllSwitches = true;

        [Tooltip("If MustActivateAllSwitches is false, this is the number of switches required")]
        public int SwitchesToCompleteObjective = 3;

        [Tooltip("Start sending notification about remaining switches when this amount is left")]
        public int NotificationSwitchesRemainingThreshold = 1;

        int m_ActivatedTotal;

        protected override void Start()
        {
            base.Start();

            EventManager.AddListener<SwitchActivatedEvent>(OnSwitchActivated);

            // set a title and description specific for this type of objective, if it hasn't one
            if (string.IsNullOrEmpty(Title))
                Title = "Activate " + (MustActivateAllSwitches ? "all the" : SwitchesToCompleteObjective.ToString()) +
                        " switches";

            if (string.IsNullOrEmpty(Description))
                Description = GetUpdatedCounterAmount();
        }

        void OnSwitchActivated(SwitchActivatedEvent evt)
        {
            if (IsCompleted)
                return;

            m_ActivatedTotal++;

            if (MustActivateAllSwitches)
                SwitchesToCompleteObjective = evt.RemainingSwitchesCount + m_ActivatedTotal;

            int targetRemaining = MustActivateAllSwitches ? evt.RemainingSwitchesCount : SwitchesToCompleteObjective - m_ActivatedTotal;

            // update the objective text according to how many switches remain to activate
            if (targetRemaining == 0)
            {
                CompleteObjective(string.Empty, GetUpdatedCounterAmount(), "Objective complete : " + Title);
            }
            else if (targetRemaining == 1)
            {
                string notificationText = NotificationSwitchesRemainingThreshold >= targetRemaining
                    ? "One switch left"
                    : string.Empty;
                UpdateObjective(string.Empty, GetUpdatedCounterAmount(), notificationText);
            }
            else
            {
                // create a notification text if needed
                string notificationText = NotificationSwitchesRemainingThreshold >= targetRemaining
                    ? targetRemaining + " switches left to activate"
                    : string.Empty;

                UpdateObjective(string.Empty, GetUpdatedCounterAmount(), notificationText);
            }
        }

        string GetUpdatedCounterAmount()
        {
            return m_ActivatedTotal + " / " + SwitchesToCompleteObjective;
        }

        void OnDestroy()
        {
            EventManager.RemoveListener<SwitchActivatedEvent>(OnSwitchActivated);
        }
    }

    public class SwitchActivatedEvent : GameEvent
    {
        public int RemainingSwitchesCount;

        public SwitchActivatedEvent(int remainingCount)
        {
            RemainingSwitchesCount = remainingCount;
        }
    }
}
