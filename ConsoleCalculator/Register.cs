using System;
using System.Collections.Generic;

namespace ConsoleCalculator
{
    public sealed class Register
    {
        private Register()
        {   
        }
        
        private static readonly Lazy<Register> Lazy = new Lazy<Register>(() => new Register());  
        public static Register Instance => Lazy.Value;

        public string CurrentId { get; set; } 
        
        public List<Cell> MyRegister { get; } = new List<Cell>();
    }
}