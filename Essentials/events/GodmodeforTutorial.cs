using PlayerRoles;
using LabApi.Events.CustomHandlers;
using LabApi.Events.Arguments.PlayerEvents;

namespace Essentials.Events
{
    internal class GodmodeforTutorial : CustomEventsHandler
    {
        public override void OnPlayerChangedRole(PlayerChangedRoleEventArgs ev)
        {
            if (ev.NewRole.RoleTypeId == RoleTypeId.Tutorial)
            {
                ev.Player.IsGodModeEnabled = true;
            }
            else
                ev.Player.IsGodModeEnabled = false;
            base.OnPlayerChangedRole(ev);
        }
    }
}
