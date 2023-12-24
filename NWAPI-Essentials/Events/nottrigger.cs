using PluginAPI.Core.Attributes;
using PlayerRoles;
using PluginAPI.Core;
using PluginAPI.Enums;
using CustomPlayerEffects;

namespace NWAPI_Essentials.Events
{
    internal class nottrigger
    {
        [PluginConfig]
        public Config Config;

        [PluginEvent(ServerEventType.PlayerChangeRole)]
        public void task(Player player, PlayerRoleBase oldRole, RoleTypeId newRole, RoleChangeReason reason)
        {
            if (newRole == RoleTypeId.Tutorial)
            {
               if (player.IsNoclipEnabled = true)
               {
                    PlayerEffectsController effectsController = player.ReferenceHub.playerEffectsController;
                    effectsController.EnableEffect<Invisible>();
               }
            }
        }
    }
}
