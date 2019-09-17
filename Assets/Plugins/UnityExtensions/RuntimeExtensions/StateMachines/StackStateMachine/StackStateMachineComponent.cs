using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityExtensions
{
    /// <summary>
    /// 栈状态机组件, 可作为一般状态机或子状态机使用
    /// Stack state machine component, available as a general state machine or substate machine
    /// </summary>
    /// <typeparam name="T"> 状态类型 </typeparam>
    public class StackStateMachineComponent<T> : ScriptableComponent, IStackState where T : class, IStackState
    {
        List<T> _states = new List<T>(4);
        double _currentStateTime;


        /// <summary>
        /// 栈中状态的总数
        /// The total number of states in the stack
        /// </summary>
        public int stateCount
        {
            get { return _states.Count; }
        }


        /// <summary>
        /// 当前状态持续时间
        /// Current state duration
        /// </summary>
        public float currentStateTime
        {
            get { return (float)_currentStateTime; }
        }


        /// <summary>
        /// 当前状态持续时间
        /// Current state duration
        /// </summary>
        public double currentStateTimeDouble
        {
            get { return _currentStateTime; }
        }


        /// <summary>
        /// 当前状态 (如果 stateCount 为 0 会抛出异常)
        /// Current state (if stateCount is 0, an exception will be thrown)
        /// </summary>
        public T currentState
        {
            get { return _states[_states.Count - 1]; }
        }


        /// <summary>
        /// 下方状态 (如果 stateCount 小于 2 会抛出异常)
        /// Lower state (if stateCount is less than 2, an exception will be thrown)
        /// </summary>
        public T underState
        {
            get { return _states[_states.Count - 2]; }
        }


        /// <summary>
        /// 获取栈中的状态
        /// Get the status in the stack
        /// </summary>
        public T GetState(int index)
        {
            return _states[index];
        }


#if DEBUG
        bool _duringSetting = false;
#endif


        /// <summary>
        /// 将新状态入栈
        /// Push the new state onto the stack
        /// </summary>
        public void PushState(T newState)
        {
#if DEBUG
            if (_duringSetting)
            {
                throw new Exception("Shouldn't change state inside OnExit, OnEnter or OnStatePushed event!");
            }
            _duringSetting = true;
#endif

            if (stateCount > 0) currentState?.OnExit(StackAction.Push);

            _currentStateTime = 0;
            _states.Add(newState);

            newState?.OnEnter(StackAction.Push);

            OnStatePushed(newState);

#if DEBUG
            _duringSetting = false;
#endif
        }


        /// <summary>
        /// 将当前状态出栈 (如果 stateCount 为 0 会抛出异常)
        /// Popping the current state (if the stateCount is 0, an exception will be thrown)
        /// </summary>
        public void PopState()
        {
#if DEBUG
            if (_duringSetting)
            {
                throw new Exception("Shouldn't change state inside OnExit, OnEnter or OnStatePopped event!");
            }
            _duringSetting = true;
#endif

            T originalState = currentState;

            originalState?.OnExit(StackAction.Pop);

            _states.RemoveAt(_states.Count - 1);
            _currentStateTime = 0;

            if (stateCount > 0) currentState?.OnEnter(StackAction.Pop);

            OnStatePopped(originalState);

#if DEBUG
            _duringSetting = false;
#endif
        }


        /// <summary>
        /// 将指定数量的状态出栈
        /// Pop the specified number of states
        /// </summary>
        public void PopStates(int count)
        {
            while (count > 0)
            {
                PopState();
                count--;
            }
        }


        /// <summary>
        /// 将所有状态出栈
        /// Pop all states
        /// </summary>
        public void PopAllStates()
        {
            PopStates(_states.Count);
        }


        protected virtual void OnStatePopped(T poppedState)
        {
        }


        protected virtual void OnStatePushed(T pushedState)
        {
        }


        /// <summary>
        /// 作为子状态机使用时需要实现此方法
        /// This method needs to be implemented when used as a child state machine
        /// </summary>
        public virtual void OnEnter(StackAction stackAction)
        {
        }


        /// <summary>
        /// 作为子状态机使用时需要实现此方法
        /// This method needs to be implemented when used as a child state machine
        /// </summary>
        public virtual void OnExit(StackAction stackAction)
        {
        }


        /// <summary>
        /// 更新当前状态
        /// 注意: 顶层状态机需要主动调用
        /// Update current status
        /// Note: The top state machine needs to be called actively
        /// </summary>
        public virtual void OnUpdate(float deltaTime)
        {
            _currentStateTime += deltaTime;
            if (stateCount > 0) currentState?.OnUpdate(deltaTime);
        }

    } // class StackStateMachineComponent<T>


    [AddComponentMenu("Unity Extensions/State Machines/Stack State Machine")]
    [DisallowMultipleComponent]
    public class StackStateMachineComponent : StackStateMachineComponent<BaseStackStateComponent>
    {
    }

} // namespace UnityExtensions
