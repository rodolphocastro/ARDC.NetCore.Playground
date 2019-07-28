using ARDC.NetCore.Playground.API.ViewModels.Profiles;
using System;

namespace ARDC.NetCore.Playground.API.ViewModels.Registration
{
    /// <summary>
    /// Helpers for registering the AutoMapper Profiles.
    /// </summary>
    public class ProfileRegistration
    {
        /// <summary>
        /// Get all the current mapping profiles available.
        /// </summary>
        /// <returns>An array of Profile's types</returns>
        public static Type[] GetProfiles() => new[] { typeof(GameProfile) };
    }
}
