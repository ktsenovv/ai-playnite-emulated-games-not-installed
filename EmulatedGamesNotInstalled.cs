using System;
using Playnite.SDK;
using Playnite.SDK.Events;
using Playnite.SDK.Plugins;

namespace EmulatedGamesNotInstalled
{
    public class EmulatedGamesNotInstalled : GenericPlugin
    {
        public override Guid Id => Guid.Parse("C7D4C4BB-0E57-4F9E-9C42-18E8C0B4F8A1");

        public EmulatedGamesNotInstalled(IPlayniteAPI api) : base(api)
        {
        }

        public override void OnLibraryUpdated(OnLibraryUpdatedEventArgs args)
        {
            foreach (var game in PlayniteApi.Database.Games)
            {
                if (game.Roms == null || game.Roms.Count == 0)
                {
                    continue;
                }

                if (game.IsInstalled || !game.OverrideInstallState)
                {
                    game.IsInstalled = false;
                    game.OverrideInstallState = true;
                    PlayniteApi.Database.Games.Update(game);
                }
            }
        }
    }
}
