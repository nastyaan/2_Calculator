using Microsoft.AspNetCore.Mvc;

namespace _2_Calculator.Controllers
{
    public enum Operation { Add, Subtract, Multiply, Divide }

    public class CalculatorController : Controller
    {
        private readonly MainDbContext _dbContext;

        public CalculatorController(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Calculate(double num1, double num2, Operation operation)
        {
            double result = 0;
            switch (operation)
            {
                case Operation.Add:
                    result = num1 + num2;
                    break;
                case Operation.Subtract:
                    result = num1 - num2;
                    break;
                case Operation.Multiply:
                    result = num1 * num2;
                    break;
                case Operation.Divide:
                    result = num1 / num2;
                    break;
            }

            var newRecord = new CalcModel
            {
                Num1 = num1,
                Num2 = num2,
                Operation = operation,
                Result = result
            };

            await _dbContext.CalcModels.AddAsync(newRecord);
            await _dbContext.SaveChangesAsync();

            ViewBag.Result = result;
            return View("Index");
        }
    }
}
