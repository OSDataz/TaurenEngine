/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 8:04:43
 *└────────────────────────┘*/

using System.Collections.Generic;

namespace TaurenEngine.AI
{
    /// <summary>
    /// 行为树基础节点
    ///
    /// 自定义可复用节点链可在构造函数中写节点链接
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AINode
    {
        public List<AINode> Children { get; private set; }
        public bool HasChildren { get; private set; }

        private IAIData _data;

        /// <summary>
        /// 激活节点
        /// </summary>
        /// <param name="tree"></param>
        internal virtual void Activate(IAIData data)
        {
            _data = data;
            Children?.ForEach(child => child.Activate(data));
            InitData();
        }

        /// <summary>
        /// 初始化获取数据
        /// </summary>
        public virtual void InitData() { }

        /// <summary>
        /// 执行评估
        /// </summary>
        /// <returns>true：条件不满足；false：满足</returns>
        internal abstract bool Evaluate();

        /// <summary>
        /// 执行具体逻辑
        /// </summary>
        /// <returns>true：执行完成；false：执行中</returns>
        public abstract bool Execute();

        /// <summary>
        /// 清理节点
        /// </summary>
        public virtual void Clear()
        {
            Children?.ForEach(child => child.Clear());
        }

        /// <summary>
        /// 销毁节点
        /// </summary>
        public virtual void Destroy()
        {
            if (Children != null)
            {
                Children.ForEach(child => child.Destroy());
                Children.Clear();
                Children = null;
            }

            HasChildren = false;
        }

        /// <summary>
        /// 添加子节点
        /// </summary>
        /// <param name="node"></param>
        public virtual void AddChild(AINode node)
        {
            if (node == null)
                return;

            if (Children == null)
                Children = new List<AINode>();

            Children.Add(node);
            HasChildren = true;
        }

        /// <summary>
        /// 删除子节点
        /// </summary>
        /// <param name="node"></param>
        public virtual void RemoveChild(AINode node)
        {
            if (Children == null || node == null)
                return;

            Children.Remove(node);
            HasChildren = Children.Count > 0;
        }

        /// <summary>
        /// 设置数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        protected void SetData<T>(string key, T value)
        {
            _data.SetData<T>(key, value);
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        protected T GetData<T>(string key)
        {
            return _data.GetData<T>(key);
        }
    }
}