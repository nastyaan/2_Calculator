using _2_Calculator.Kafka;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace _2_Calculator.Controllers
{

    public class CalculatorController : Controller
    {
        private readonly MainDbContext _context;
        private readonly KafkaProducerService<Null, string> _producer;

        public CalculatorController(MainDbContext context, KafkaProducerService<Null, string> producer)
        {
            _context = context;
            _producer = producer;
        }

        /// <summary>
        /// Отображение страницы Index.
        /// </summary>
        public IActionResult Index()
        {
            var data = _context.CalcModels.OrderByDescending(x => x.ID).ToList();

            return View(data);
        }

        /// <summary>
        /// Обработка запроса на вычисление.
        /// </summary>
        /// <param name="num1">Первый операнд.</param>
        /// <param name="num2">Второй операнд.</param>
        /// <param name="operation">Тип операции (сложение, вычитание, умножение, деление).</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Calculate(double num1, double num2, MathOperation operation)
        {
            // Подготовка объекта для расчета
            var dataInputVariant = new CalcModel
            {
                Num1 = num1,
                Num2 = num2,
                Operation = operation,
            };

            // Отправка данных в Kafka
            await SendDataToKafka(dataInputVariant);

            // Перенаправление на страницу Index
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Callback([FromBody] CalcModel inputData)
        {
            // Сохранение данных и результата в базе данных
            SaveDataAndResult(inputData);

            return Ok();
        }

        /// <summary>
        /// Сохранение данных и результата в базе данных.
        /// </summary>
        /// <param name="num1">Первый операнд.</param>
        /// <param name="num2">Второй операнд.</param>
        /// <param name="operation">Тип операции (сложение, вычитание, умножение, деление).</param>
        /// <param name="result">Результат математической операции.</param>
        /// <returns>Объект с данными и результатом.</returns>
        private void SaveDataAndResult(CalcModel inputData)
        {
            _context.CalcModels.Add(inputData);
            _context.SaveChanges();
        }

        /// <summary>
        /// Отправка данных в Kafka.
        /// </summary>
        /// <param name="dataInputVariant">Объект с данными и результатом.</param>
        /// <returns>Task.</returns>
        private async Task SendDataToKafka(CalcModel dataInputVariant)
        {
            var json = JsonSerializer.Serialize(dataInputVariant);
            await _producer.ProduceAsync("Anisimova", new Message<Null, string> { Value = json });
        }
    }
}
