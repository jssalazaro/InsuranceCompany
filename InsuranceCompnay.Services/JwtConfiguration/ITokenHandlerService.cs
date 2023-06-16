using InsuranceCompnay.Abstractions;

namespace InsuranceCompnay.Services.JwtConfiguration
{
    public interface ITokenHandlerService
    {
        string GenerateJwtToken(ITokenParameters pars);
    }
}
