using System.Collections.Generic;
using NHibernate.Shards;
using NHibernate.Shards.LoadBalance;
using NHibernate.Shards.Strategy;
using NHibernate.Shards.Strategy.Access;
using NHibernate.Shards.Strategy.Resolution;
using NHibernate.Shards.Strategy.Selection;

namespace Eg.Shards.Runner
{
  public class ShardStrategyFactory : IShardStrategyFactory
  {

    public IShardStrategy NewShardStrategy(
      ICollection<ShardId> shardIds)
    {
      return new ShardStrategyImpl(
        GetSelectionStrategy(shardIds),
        GetResolutionStrategy(shardIds),
        GetAccessStrategy(shardIds));
    }

    private static IShardSelectionStrategy 
      GetSelectionStrategy(
        ICollection<ShardId> shardIds)
    {
      var loadBalancer =
        new RoundRobinShardLoadBalancer(shardIds);
      return new RoundRobinShardSelectionStrategy(
        loadBalancer);
    }

    private static IShardResolutionStrategy 
      GetResolutionStrategy(
        ICollection<ShardId> shardIds)
    {
      return new AllShardsShardResolutionStrategy(
        shardIds);
    }

    private static IShardAccessStrategy 
      GetAccessStrategy(
        ICollection<ShardId> shardIds)
    {
      return new SequentialShardAccessStrategy();
    }



  }
}
