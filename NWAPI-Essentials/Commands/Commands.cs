using CommandSystem;
using System;

namespace NWAPI_Essentials.Commands
{
    internal class Commands
    {
        [CommandHandler(typeof(RemoteAdminCommandHandler))]
        public class BetterDropsCommand : ParentCommand
        {
            public BetterDropsCommand() => LoadGeneratedCommands();

            public sealed override void LoadGeneratedCommands()
            {
                RegisterCommand(TPS.Instance);
                RegisterCommand(Invis.Instance);
                RegisterCommand(Visible.Instance);
                RegisterCommand(Freeze.Instance);
                RegisterCommand(UnFreeze.Instance);
                RegisterCommand(Size.Instance);
            }

            protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
            {
                response = "Essentials commands: TPS, Invis, Freeze, UnFreeze, Size";
                return false;
            }

            public override string Command { get; } = "Et";
            public override string[] Aliases { get; } = Array.Empty<string>();
            public override string Description { get; } = "EssentialsCommands.";
        }
    }
}
