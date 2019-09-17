using System;
using UnityEngine;
using UnityEngine.Events;

namespace UnityExtensions
{
    /// <summary>
    /// 栈状态组件基类
    /// Stack state component base class
    /// </summary>
    public abstract class BaseStackStateComponent : ScriptableComponent, IStackState
    {
        public abstract void OnEnter(StackAction stackAction);
        public abstract void OnExit(StackAction stackAction);
        public abstract void OnUpdate(float deltaTime);
    }


    /// <summary>
    /// 栈状态组件. 状态的 Enter 和 Exit 事件可序列化
    /// Stack state component. The Enter and Exit events of the state can be serialized
    /// </summary>
    [AddComponentMenu("Unity Extensions/State Machines/Stack State")]
    [DisallowMultipleComponent]
    public class StackStateComponent : BaseStackStateComponent
    {
        [SerializeField]
        StackStateEvent _onEnter;

        [SerializeField]
        StackStateEvent _onExit;


        /// <summary>
        /// 添加或移除更新状态触发的事件
        /// Add or remove events triggered by update status
        /// </summary>
        public event Action<float> onUpdate;


        /// <summary>
        /// 添加或移除进入状态触发的事件
        /// Add or remove events that are triggered by the incoming state
        /// </summary>
        public event UnityAction<StackAction> onEnter
        {
            add
            {
                if (_onEnter == null) _onEnter = new StackStateEvent();
                _onEnter.AddListener(value);
            }
            remove { _onEnter?.RemoveListener(value); }
        }


        /// <summary>
        /// 添加或移除离开状态触发的事件
        /// Add or remove events triggered by leaving status
        /// </summary>
        public event UnityAction<StackAction> onExit
        {
            add
            {
                if (_onExit == null) _onExit = new StackStateEvent();
                _onExit.AddListener(value);
            }
            remove { _onExit?.RemoveListener(value); }
        }


        public override void OnEnter(StackAction stackAction)
        {
            _onEnter?.Invoke(stackAction);
        }


        public override void OnExit(StackAction stackAction)
        {
            _onExit?.Invoke(stackAction);
        }


        public override void OnUpdate(float deltaTime)
        {
            onUpdate?.Invoke(deltaTime);
        }

    } // class StackStateComponent

} // namespace UnityExtensions
