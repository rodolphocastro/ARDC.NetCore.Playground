using ARDC.NetCore.Playground.API.ViewModels.GameViewModels;
using ARDC.NetCore.Playground.Domain.Models;
using AutoMapper;

namespace ARDC.NetCore.Playground.API.ViewModels.Profiles
{
    /// <summary>
    /// AutoMapper config for Games and its ViewModels.
    /// </summary>
    public class GameProfile : Profile
    {
        public GameProfile()
        {
            // Single way mappings, from ViewModel to Model
            CreateMap<GameCreate, Game>();

            // Both way mappings
            CreateMap<Game, GameEdit>().ReverseMap();

            // Single way mappings, from Model to ViewModel
            CreateMap<Game, GameList>();
            CreateMap<Game, GameView>();
        }
    }
}
