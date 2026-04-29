using Microsoft.Extensions.Options;

namespace SiteCasamento.Services
{
    public class PixService
    {
        private readonly PixOptions _options;

        public PixService(IOptions<PixOptions> options)
        {
            _options = options.Value;
        }

        public string ObterPayload()
        {
            if (string.IsNullOrWhiteSpace(_options.PayloadFixo))
                throw new InvalidOperationException("PIX não configurado.");

            return _options.PayloadFixo;
        }
    }
}