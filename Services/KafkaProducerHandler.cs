using Confluent.Kafka;
using Microsoft.Extensions.Options;

namespace _2_Calculator.Kafka
{
    public class KafkaProducerHandler : IDisposable
    {
        private IProducer<byte[], byte[]> _kafkaProducer;

        public KafkaProducerHandler(IConfiguration configuration)
        {
            var producerConf = new ProducerConfig();

            configuration.GetSection("Kafka:ProducerSettings").Bind(producerConf);

            _kafkaProducer = new ProducerBuilder<byte[], byte[]>(producerConf).Build();
        }

        public Handle Handle => _kafkaProducer.Handle;

        public void Dispose()
        {
            _kafkaProducer.Flush();
            _kafkaProducer.Dispose();
        }
    }
}
