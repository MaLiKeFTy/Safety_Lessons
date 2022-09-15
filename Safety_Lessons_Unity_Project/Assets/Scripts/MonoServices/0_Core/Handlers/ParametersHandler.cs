using System.Collections.Generic;

namespace MonoServices.Core
{
    public abstract class ParametersHandler : MonoService
    {
        public abstract bool isInvoker { get; } 
        public abstract List<string> ParameterNames();
    }
}

