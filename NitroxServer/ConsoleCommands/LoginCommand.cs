﻿using NitroxServer.ConsoleCommands.Abstract;
using NitroxModel.DataStructures.GameLogic;
using NitroxServer.GameLogic;
using NitroxModel.DataStructures.Util;
using NitroxModel.Logger;
using NitroxServer.ConfigParser;

namespace NitroxServer.ConsoleCommands
{
    internal class LoginCommand : Command
    {
        private readonly ServerConfig serverConfig;

        public LoginCommand(ServerConfig serverConfig) : base("login", Perms.PLAYER, "{password}", "Log in to server as admin (requires password)")
        {
            this.serverConfig = serverConfig;
        }

        public override void RunCommand(string[] args, Optional<Player> sender)
        {
            string message = "Can't update permissions";

            if (sender.IsPresent())
            {
                if (args[0] == serverConfig.AdminPassword)
                {
                    sender.Get().Permissions = Perms.ADMIN;
                    message = $"Updated permissions to admin for {sender.Get().Name}";
                }
                else
                {
                    message = "Incorrect Password";
                }
            }

            Notify(sender, message);
        }

        public override bool VerifyArgs(string[] args)
        {
            return args.Length == 1;
        }
    }
}
