﻿using CommandSystem;
using Essentials.commands;
using System;

namespace Essentials.Commands
{
    internal class Commands
    {
        [CommandHandler(typeof(RemoteAdminCommandHandler))]
        [CommandHandler(typeof(GameConsoleCommandHandler))]
        [CommandHandler(typeof(ClientCommandHandler))]
        public class CommandsEs : ParentCommand
        {
            public CommandsEs() => LoadGeneratedCommands();
            public sealed override void LoadGeneratedCommands()
            {
                RegisterCommand(TPS.Instance);
                RegisterCommand(Invis.Instance);
                RegisterCommand(Visible.Instance);
                RegisterCommand(Freeze.Instance);
                RegisterCommand(UnFreeze.Instance);
                RegisterCommand(Size.Instance);
                RegisterCommand(Cheatcheckpassed.Instance);
                RegisterCommand(CheatCheck.Instance);
                RegisterCommand(AdminLog.Instance);
                RegisterCommand(Warn.Instance);
                RegisterCommand(Showhint.Instance);
                RegisterCommand(PosTP.Instance);
                RegisterCommand(CustomInfo.Instance);
                RegisterCommand(Disco.Instance);
                RegisterCommand(FakeSize.Instance);
            }
            protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
            {
                response = "\nPlease enter a valid subcommand:";

                foreach (ICommand command in AllCommands)
                {
                   response += $"\n\n<color=yellow><b>- {command.Command} ({string.Join(", ", command.Aliases)})</b></color>\n<color=white>{command.Description}</color>";
                }
                return false;
            }
            public override string Command { get; } = "Et";
            public override string[] Aliases { get; } = Array.Empty<string>();
            public override string Description { get; } = "EssentialsCommands.";
        }
    }
}