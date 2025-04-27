using LabApi.Events.CustomHandlers;
using LabApi.Features.Wrappers;
using LabApi.Events.Arguments.ServerEvents;

namespace Essentials.Events
{
    internal class autoffroggle : CustomEventsHandler
    {
        public override void OnServerRoundStarted()
        {
            Server.FriendlyFire = false;
            base.OnServerRoundStarted();
        }
        public override void OnServerRoundEnded(RoundEndedEventArgs ev)
        {
            Server.FriendlyFire = true;
            base.OnServerRoundEnded(ev);
        }
    }
}