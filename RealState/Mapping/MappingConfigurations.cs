using Mapster;
using RealState.Contracts.Property;
using RealState.Entities;
using RealState.Entities.PropertForRent;


namespace RealState.Mapping
{
    public class MappingConfigurations : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RealState.Contracts.Auth.RegisterRequest, ApplicationUser>().Map(dest => dest.UserName, src => src.Email).Map(dest => dest.EmailConfirmed, src => true);

            config.NewConfig<PropertForSell, PropertyForSellResponse>().Ignore(dest => dest.Images);
            config.NewConfig<PropertForRent, PropertyForRentResponse>().Ignore(dest => dest.Images);


           

        }
    }
}
