using PluginAPI.Core.Attributes;
using PluginAPI.Core;
using PluginAPI.Enums;
using PlayerRoles;
using GameCore;
using System.IO;
using System;
using PluginAPI.Roles;
using YamlDotNet.Serialization;
using static MapGeneration.ImageGenerator;

namespace NWAPI_Essentials
{
    internal class Events
    {
        [PluginConfig]
        public Config Config;

        [PluginEvent(ServerEventType.PlayerChangeRole)]
        public void On(Player player, PlayerRoleBase oldRole, RoleTypeId newRole, RoleChangeReason reason)
        {
            if (!Config.Tutorialgodmode)
                return;
            if (newRole == RoleTypeId.Tutorial)
            {
                player.IsGodModeEnabled = true;
            }
            else
                player.IsGodModeEnabled = false;
        }
    }
}

