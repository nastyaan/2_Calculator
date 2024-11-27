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
        
        public MathOperation Operation { get; set; }

        public double Result { get; set; }

        public void CalculateOperation()
        {
            Result = Operation switch
            {
                MathOperation.Add => Num1 + Num2,
                MathOperation.Subtract => Num1 - Num2,
                MathOperation.Divide => Num1 / Num2,
                MathOperation.Multiply => Num1 * Num2,
                _ => throw new NotImplementedException()
            };
        }
    }
}
