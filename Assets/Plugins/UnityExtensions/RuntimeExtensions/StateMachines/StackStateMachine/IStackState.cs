
namespace UnityExtensions
{
    /// <summary>
    /// 状态栈的行为
    /// State stack behavior
    /// </summary>
    public enum StackAction
    {
        Push,
        Pop,
    }


    /// <summary>
    /// 栈状态接口
    /// Stack status interface
    /// </summary>
    public interface IStackState
    {
        /// <summary>
        /// 进入状态时触发
        /// Trigger when entering state
        /// </summary>
        void OnEnter(StackAction stackAction);

        /// <summary>
        /// 离开状态时触发
        /// Triggered when leaving state
        /// </summary>
        void OnExit(StackAction stackAction);

        /// <summary>
        /// 更新状态时触发
        /// Triggered when updating status
        /// </summary>
        void OnUpdate(float deltaTime);

    } // interface IStackState

} // namespace UnityExtensions
