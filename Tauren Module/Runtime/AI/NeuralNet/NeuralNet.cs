/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.1.0
 *│　Time    ：2021/8/29 8:15:50
 *└────────────────────────┘*/

using System.Collections.Generic;

namespace Tauren.Module.Runtime
{
    /// <summary>
    /// 神经网络
    ///
    /// 扩展：后期可以考虑 响应时间 关联神经节点数量*获取神经节点权重/关联神经节点总权重
    /// </summary>
    public class NeuralNet<T> where T : class, INeuralInfo
    {
        private readonly Dictionary<string, NeuralNode<T>> _nodes = new Dictionary<string, NeuralNode<T>>();

        /// <summary>
        /// 添加刺激反馈信息
        /// </summary>
        /// <param name="infoA"></param>
        /// <param name="infoB"></param>
        /// <param name="weight"></param>
        public void AddInfo(T infoA, T infoB, float weight)
        {
            GetNode(infoA).AddNodeBrige(GetNode(infoB), weight);
        }

        /// <summary>
        /// 获取链接权重最高的神经节点
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public T GetInfoWeight(T info)
        {
            return GetNode(info).GetInfoWeight();
        }

        /// <summary>
        /// 获取链接权重最高的神经节点
        /// </summary>
        /// <param name="info"></param>
        /// <param name="flag">特征标记</param>
        /// <returns></returns>
        public T GetInfoWeight(T info, string flag)
        {
            return GetNode(info).GetInfoWeight(flag);
        }

        /// <summary>
        /// 获取正向期望最高的神经节点
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public T GetInfoPositive(T info)
        {
            return GetNode(info).GetInfoPositive();
        }

        /// <summary>
        /// 获取正向期望最高的神经节点
        /// </summary>
        /// <param name="info"></param>
        /// <param name="flag">特征标记</param>
        /// <returns></returns>
        public T GetInfoPositive(T info, string flag)
        {
            return GetNode(info).GetInfoPositive(flag);
        }

        /// <summary>
        /// 获取负向期望最高的神经节点
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public T GetInfoNegative(T info)
        {
            return GetNode(info).GetInfoNegative();
        }

        /// <summary>
        /// 获取负向期望最高的神经节点
        /// </summary>
        /// <param name="info"></param>
        /// <param name="flag">特征标记</param>
        /// <returns></returns>
        public T GetInfoNegative(T info, string flag)
        {
            return GetNode(info).GetInfoNegative(flag);
        }

        private NeuralNode<T> GetNode(T info)
        {
            var node = _nodes[info.Type];
            if (node == null)
            {
                node = new NeuralNode<T>();
                node.Info = info;
                _nodes[info.Type] = node;
            }

            return node;
        }
    }
}