using GameBackendService.Brokers.Infrastructure;
using GameBackendService.Brokers.Kafka;
using Microsoft.Extensions.DependencyInjection;

namespace GameBackendService.Brokers.Kafka;

public class KafkaConsumerService : KafkaConsumerService<KafkaMessageHandlersController>
{
    public KafkaConsumerService(IServiceScopeFactory serviceScopeFactory, KafkaOptions kafkaOptions)
        : base(serviceScopeFactory, kafkaOptions) { }
}
