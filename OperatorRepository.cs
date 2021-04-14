using System.Collections.Generic;

namespace Asamblor
{
    public class OperatorRepository
    {
        public Dictionary<string, string> Operators { get; private set; }

        public OperatorRepository()
        {
            Operators = new Dictionary<string, string>();
            AddOperators();
        }

        private void Create2OperandOperators()
        {
            Operators.Add("mov", "0000");
            Operators.Add("add", "0001");
            Operators.Add("sub", "0010");
            Operators.Add("cmp", "0011");
            Operators.Add("and", "0100");
            Operators.Add("or", "0101");
            Operators.Add("xor", "0110");
        }

        private void Create1OperandOperators()
        {
            Operators.Add("clr", "1000000000");
            Operators.Add("neg", "1000000001");
            Operators.Add("inc", "1000000011");
            Operators.Add("dec", "1000000010");
            Operators.Add("asl", "1000000100");
            Operators.Add("asr", "1000000101");
            Operators.Add("lsr", "1000000110");
            Operators.Add("rol", "1000000111");
            Operators.Add("ror", "1000001000");
            Operators.Add("rlc", "1000001001");
            Operators.Add("rrc", "1000001010");
            Operators.Add("jmp", "1000001011");
            Operators.Add("call", "1000001100");
            Operators.Add("push", "1000001101");
            Operators.Add("pop", "1000001110");
        }

        private void CreateBranchOperators()
        {
            Operators.Add("br", "11000000");
            Operators.Add("bne", "11000001");
            Operators.Add("beq", "11000010");
            Operators.Add("bpl", "11000011");
            Operators.Add("bmi", "11000100");
            Operators.Add("bcs", "11000101");
            Operators.Add("bcc", "11000110");
            Operators.Add("bvs", "11000111");
            Operators.Add("bvc", "11001000");
        }

        private void CreateOtherOperators()
        {
            Operators.Add("clc", "1110000000000000");
            Operators.Add("clv", "1110000000000001");
            Operators.Add("clz", "1110000000000010");
            Operators.Add("cls", "1110000000000011");
            Operators.Add("ccc", "1110000000000100");
            Operators.Add("sec", "1110000000000101");
            Operators.Add("sev", "1110000000000110");
            Operators.Add("sez", "1110000000000111");
            Operators.Add("ses", "1110000000001000");
            Operators.Add("scc", "1110000000001001");
            Operators.Add("nop", "1110000000001010");
            Operators.Add("ret", "1110000000001011");
            Operators.Add("reti", "1110000000001100");
            Operators.Add("halt", "1110000000001101");
            Operators.Add("wait", "1110000000001110");
            Operators.Add("pushpc", "1110000000001111");
            Operators.Add("poppc", "1110000000010000");
            Operators.Add("pushflag", "1110000000010001");
            Operators.Add("popflag", "1110000000100010");
        }

        private void AddOperators()
        {
            Create2OperandOperators();
            Create1OperandOperators();
            CreateBranchOperators();
            CreateOtherOperators();
        }

        public string GetValue(string key)
        {
            return Operators.ContainsKey(key) ? Operators[key] : "";
        }
    }
}