using HotFix.GamePlay;
using UnityEngine;
using UnityEngine.UI;

namespace TestRun
{
    public class TestRun : MonoBehaviour
    {
        public Button startBtn;
        public Button endBtn;

        private void Start()
        {
            startBtn.onClick.AddListener(OnTestRunStart);
            endBtn.onClick.AddListener(OnTestRunEnd);
        }
        
        private void Update()
        {
            var deltaTime = Time.deltaTime;
        }

        private void FixedUpdate()
        {
            var deltaTime = Time.fixedDeltaTime;
        }

        private void LateUpdate()
        {
            var deltaTime = Time.deltaTime;
        }

        private void OnTestRunStart()
        {
        }
        
        private void OnTestRunEnd()
        {
        }
    }
    
    // 测试用的实体类
    public class TestEntity : EntityBase { }
}