﻿using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Valour.Client.Planets
{
    public class ClientPlanetManager
    {
        /// <summary>
        /// The currently focused planet
        /// </summary>
        private ClientPlanet Current { get; set; }

        public event Func<Task> OnPlanetChange;

        public event Func<Task> OnChannelsUpdate;

        public event Func<Task> OnChannelWindowUpdate;

        public async Task RefreshOpenedChannels() {
            await OnChannelWindowUpdate.Invoke();
        }

        public async Task RefreshChannels() {
            await OnChannelsUpdate.Invoke();
        }

        public async Task SetCurrentPlanet(ClientPlanet planet)
        {
            if (planet == null || (Current != null && Current.Id == planet.Id)) return;

            Current = planet;

            Console.WriteLine($"Set current planet to {planet.Id}");

            if (OnPlanetChange != null)
            {
                Console.WriteLine($"Invoking planet change event");
                await OnPlanetChange?.Invoke();
            }
        }

        public ClientPlanet GetCurrent()
        {
            return Current;
        }

        public override string ToString()
        {
            return $"Planet: {Current.Id}";
        }
    }
}
