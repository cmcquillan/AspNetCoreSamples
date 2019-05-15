using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Config
{
    public class ClientOptions : IConfigureOptions<ClientOptions>
    {
        private readonly IConfiguration _configuration;

        public ClientOptions(IConfiguration configuration) {
            _configuration = configuration;
        }

        public ClientOptions() { }

        public void Configure(ClientOptions options) => _configuration.Bind(options);

        public string Authority { get; set; }

        public string Api { get; set; }

        public string Scopes { get; set; }
    }
}
