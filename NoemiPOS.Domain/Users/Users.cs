using NoemiPOS.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoemiPOS.Domain.Users;
public sealed class Users : BaseTenantEntity
{
    private Users(Guid id, Guid tenantId) : base(id, tenantId)
    {

    }
}
