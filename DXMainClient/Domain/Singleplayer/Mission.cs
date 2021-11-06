﻿using Rampastring.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DTAClient.Domain.Singleplayer
{
    /// <summary>
    /// A Tiberian Sun mission listed in Battle(E).ini or INI/Campaigns.ini.
    /// </summary>
    public class Mission
    {
        public Mission(IniSection iniSection, bool isCampaignMission)
        {
            InternalName = iniSection.SectionName;
            Side = iniSection.GetIntValue(nameof(Side), 0);
            Scenario = iniSection.GetStringValue(nameof(Scenario), string.Empty);
            GUIName = iniSection.GetStringValue("Description", "Undefined mission");
            if (iniSection.KeyExists("UIName"))
                GUIName = iniSection.GetStringValue("UIName", GUIName);

            IconPath = iniSection.GetStringValue(nameof(IconPath), string.Empty);
            GUIDescription = iniSection.GetStringValue("LongDescription", string.Empty);
            RequiredAddon = iniSection.GetBooleanValue(nameof(RequiredAddon), false);
            Enabled = iniSection.GetBooleanValue(nameof(Enabled), true);
            BuildOffAlly = iniSection.GetBooleanValue(nameof(BuildOffAlly), false);
            PlayerAlwaysOnNormalDifficulty = iniSection.GetBooleanValue(nameof(PlayerAlwaysOnNormalDifficulty), false);

            CampaignInternalName = iniSection.GetStringValue(nameof(CampaignInternalName), null);
            GlobalVariables = iniSection.GetStringValue(nameof(GlobalVariables), string.Empty).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            RequiresUnlocking = iniSection.GetBooleanValue(nameof(RequiresUnlocking), isCampaignMission);
            UnlockMissions = iniSection.GetStringValue(nameof(UnlockMissions), string.Empty).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            UsedGlobalVariables = iniSection.GetStringValue(nameof(UsedGlobalVariables), string.Empty).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            UnlockGlobalVariables = iniSection.GetStringValue(nameof(UnlockGlobalVariables), string.Empty).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            // Parse conditional mission unlocks
            int i = 0;
            while (true)
            {
                string conditionalMissionUnlockData = iniSection.GetStringValue("ConditionalMissionUnlock" + i, null);
                if (string.IsNullOrWhiteSpace(conditionalMissionUnlockData))
                    break;

                var conditionalMissionUnlock = ConditionalMissionUnlock.FromString(conditionalMissionUnlockData);
                if (conditionalMissionUnlock != null)
                    ConditionalMissionUnlocks.Add(conditionalMissionUnlock);

                i++;
            }

            GUIDescription = GUIDescription.Replace("@", Environment.NewLine);
        }

        public string InternalName { get; }
        public int Side { get; }
        public string Scenario { get; }
        public string GUIName { get; }
        public string IconPath { get; }
        public string GUIDescription { get; }
        public bool RequiredAddon { get; }
        public bool Enabled { get; }
        public bool BuildOffAlly { get; }
        public bool PlayerAlwaysOnNormalDifficulty { get; }

        /// <summary>
        /// If not null, this is not a mission but a dummy entry for a campaign.
        /// </summary>
        public string CampaignInternalName { get; }

        /// <summary>
        /// The global variables relevant to this mission.
        /// </summary>
        public List<string> GlobalVariables { get; private set; } = new List<string>(0);

        /// <summary>
        /// Is this a mission that is unlocked by playing other missions?
        /// </summary>
        public bool RequiresUnlocking { get; private set; }

        /// <summary>
        /// If this is a mission that requires unlocking,
        /// has the player unlocked this mission?
        /// </summary>
        public bool IsUnlocked { get; private set; }

        /// <summary>
        /// The internal names of missions that winning this mission unlocks
        /// directly.
        /// </summary>
        public string[] UnlockMissions { get; private set; }

        public List<ConditionalMissionUnlock> ConditionalMissionUnlocks { get; } = new List<ConditionalMissionUnlock>(0);

        /// <summary>
        /// The global variables that this mission utilizes.
        /// </summary>
        public string[] UsedGlobalVariables { get; private set; }

        /// <summary>
        /// The global variables that winning this mission unlocks.
        /// </summary>
        public string[] UnlockGlobalVariables { get; private set; }
    }
}
