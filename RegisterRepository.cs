using System.Collections.Generic;

namespace Asamblor
{
    public class RegisterRepository
    {
        public Dictionary<string, string> Registers { get; set; }

        public RegisterRepository()
        {
            Registers = new Dictionary<string, string>();
            AddRegisters();
        }

        private void AddRegisters()
        {
            Registers.Add("r0", "0000");
            Registers.Add("r1", "0001");
            Registers.Add("r2", "0010");
            Registers.Add("r3", "0011");
            Registers.Add("r4", "0100");
            Registers.Add("r5", "0101");
            Registers.Add("r6", "0110");
            Registers.Add("r7", "0111");
            Registers.Add("r8", "1000");
            Registers.Add("r9", "1001");
            Registers.Add("r10", "1010");
            Registers.Add("r11", "1011");
            Registers.Add("r12", "1100");
            Registers.Add("r13", "1101");
            Registers.Add("r14", "1110");
            Registers.Add("r15", "1111");
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