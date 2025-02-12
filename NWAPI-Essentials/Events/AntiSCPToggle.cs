using LabApi.Events.Arguments.Scp096Events;
using LabApi.Events.Arguments.Scp173Events;
using LabApi.Events.CustomHandlers;

namespace NWAPI_Essentials.Events
{
    internal class AntiSCPToggle : CustomEventsHandler
    {
        public override void OnScp173AddingObserver(Scp173AddingObserverEventArgs ev)
        {
            if (ev.Target.Role == PlayerRoles.RoleTypeId.Tutorial)
            {
                ev.IsAllowed = false;
            }
            base.OnScp173AddingObserver(ev);
        }
        public override void OnScp096AddingTarget(Scp096AddingTargetEventArgs ev)
        {
            if (ev.Target.Role == PlayerRoles.RoleTypeId.Tutorial)
            {
                ev.IsAllowed = false;
            }
            base.OnScp096AddingTarget(ev);
        }
    }
}
