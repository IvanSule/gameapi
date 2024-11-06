using RPSSL.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPSSL.Application.Choices
{
    public sealed record ChoiceResponse(RPSSLOptions Id, string Name);
}
