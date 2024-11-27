using Confluent.Kafka;

namespace _2_Calculator.Kafka
{
    public class KafkaProducerService<TKey, TValue>
    {
        IProducer<TKey, TValue> _kafkaHandle;

        public KafkaProducerService(KafkaProducerHandler producerHandler)
        {
            _kafkaHandle = new DependentProducerBuilder<TKey, TValue>(producerHandler.Handle).Build();
        }

        public Task ProduceAsync(string topic, Message<TKey, TValue> message) => _kafkaHandle.ProduceAsync(topic, message);

        public void Produce(string topic, Message<TKey, TValue> message, Action<DeliveryReport<TKey, TValue>>? deliveryHandler = null)
            => _kafkaHandle.Produce(topic, message, deliveryHandler);

        public void Flush(TimeSpan timeout) => _kafkaHandle.Flush(timeout);
    }
}
