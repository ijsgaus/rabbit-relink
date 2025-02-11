﻿using System.Collections.Immutable;
using RabbitRelink.Consumer;
using RabbitRelink.Messaging;
using RabbitRelink.Middlewares;
using RabbitRelink.Topology;

namespace RabbitRelink;

internal class ConsumerHandlerBuilder<T> : IConsumerHandlerBuilder<T> where T : class?
{
    private readonly Func<DoConsume<T>, IRelinkConsumer> _factory;

    public ConsumerHandlerBuilder(Func<DoConsume<T>, IRelinkConsumer> factory)
        => _factory = factory;

    public IRelinkConsumer Handler(DoConsume<T> handler)
        => _factory(handler);

    public IConsumerHandlerBuilder<TOut> Middleware<TOut>(ConsumerMiddleware<TOut, T> middleware) where TOut : class?
        => new ConsumerHandlerBuilder<TOut>(handler => _factory(middleware(handler)));
}
