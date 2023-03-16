using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchManager.Models
{
    public class BatchCommand
    {
        public string Name { get; set; }
        public string Command { get; set; }
        public BatchCommand(string name, string command)
        {
            Name = name;
            Command = command;
        }
    }
}
