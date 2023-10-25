using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocVisionClient;

public class Message
{
    public string Title { get; set; }
    public DateTime SendDate { get; set; }
    public string Addressee{ get; set; }
    public string Sender { get; set; }
    public string Content { get; set; }
}
