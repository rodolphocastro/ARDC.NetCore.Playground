using ARDC.NetCore.Playground.API.ViewModels.ReviewViewModels;
using ARDC.NetCore.Playground.Domain.Models;
using AutoMapper;

namespace ARDC.NetCore.Playground.API.ViewModels.Profiles
{
    /// <summary>
    /// AutoMapper config for Reviews and its ViewModels.
    /// </summary>
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            // Single way mappings from ViewModels to Model
            CreateMap<ReviewCreate, Review>();

            // Both ways mappings
            CreateMap<Review, ReviewEdit>().ReverseMap();

            // Single way mappings, from Model to ViewModel
            CreateMap<Review, ReviewList>();
            CreateMap<Review, ReviewView>();
        }
    }
}
