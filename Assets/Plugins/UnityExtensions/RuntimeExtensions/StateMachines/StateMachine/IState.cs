
namespace UnityExtensions
{
    /// <summary>
    /// 状态接口
    /// Status interface
    /// </summary>
    public interface IState
    {
        /// <summary>
        /// 进入状态时触发
        /// Trigger when entering state
        /// </summary>
        void OnEnter();

        /// <summary>
        /// 离开状态时触发
        /// Triggered when leaving state
        /// </summary>
        void OnExit();

        /// <summary>
        /// 更新状态时触发
        /// Triggered when updating status
        /// </summary>
        void OnUpdate(float deltaTime);

    } // interface IState

} // namespace UnityExtensions
