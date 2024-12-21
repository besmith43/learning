using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace plugin.contract
{
    public class GetPlugin
    {
        public string Statement { get; set; }
    }

    public interface IGetPlugin
    {
        Task<IEnumerable<GetPlugin>> Run();
    }
}
