using System.Collections.Generic;

namespace Asamblor
{
    public class RegisterRepository
    {
        private Dictionary<string, string> Registers { get; set; }

        public RegisterRepository()
        {
            Registers = new Dictionary<string, string>();
            AddRegisters();
        }

        private void AddRegisters()
        {
            Registers.Add("R0", "0000");
            Registers.Add("R1", "0001");
            Registers.Add("R2", "0010");
            Registers.Add("R3", "0011");
            Registers.Add("R4", "0100");
            Registers.Add("R5", "0101");
            Registers.Add("R6", "0110");
            Registers.Add("R7", "0111");
            Registers.Add("R8", "1000");
            Registers.Add("R9", "1001");
            Registers.Add("R10", "1010");
            Registers.Add("R11", "1011");
            Registers.Add("R12", "1100");
            Registers.Add("R13", "1101");
            Registers.Add("R14", "1110");
            Registers.Add("R15", "1111");
        }

        public string GetValue(string key)
        {
            return Registers.ContainsKey(key) ? Registers[key] : "";
        }

        public bool Contains(string keyValue)
        {
            return Registers.ContainsKey(keyValue);
        }
    }
}