using _2_Calculator.Controllers;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Emit;

namespace _2_Calculator
{
    public class CalcModel
    {
        [Key]
        public int ID { get; set; }
        
        public double Num1 { get; set; }

        public double Num2 { get; set; }
        
        public Operation Operation { get; set; }

        public double Result { get; set; }
    }
}
