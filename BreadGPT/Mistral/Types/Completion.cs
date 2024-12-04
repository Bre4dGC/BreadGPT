using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mistral.Types;

public class Completion
{
    public string Model { get; set; }
    public Message[] Messages { get; set; }
}
