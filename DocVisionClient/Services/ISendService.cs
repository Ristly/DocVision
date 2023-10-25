using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocVisionClient.Services;

public interface ISendService
{
    public Task<bool> SendMessage(Message message);
}
