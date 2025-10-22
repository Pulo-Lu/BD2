using System;
using System.Collections.Generic;
using HotFix.Enums;
using HotFix.Struct;
using HotFix.Tools.Extension;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace HotFix.Data.SO.CharacterModelMapping
{
    [CreateAssetMenu(fileName = "NewFile", menuName = "游戏配置/1_模型配置/创建角色模型映射文件", order = 1)]
    public class CharactarModelMappingConfigSO : ScriptableObject
    {
        [Header("id")]
        [InlineButton("一键初始化")]
        public int characterId;
        
        [Header("名称")]
        public string characterName;
        
        [Header("职业")]
        public EnumCharacterJob job = EnumCharacterJob.Guard;

        [Header("分支")]
        public EnumCharacterJobSub Sub = EnumCharacterJobSub.Fighter;
        
        [Header("基础属性")]
        [InlineButton("随机初始化基础属性")]
        public List<PropertiesStruct> baseProperties = new();
        
#if UNITY_EDITOR
        public void 一键初始化() {
            // 从 name中 获取出 怪物的数字ID
            string[] split = name.Split('_');
            if (split.Length >= 2) {
                characterId = split[0].ToInt();
                characterName = split[1];
            } else {
                Debug.LogWarning($"CharactarModelMappingConfigSO Init failed, name format should be 'id_name', but got '{name}'");
            }
        }
        
        public void 随机初始化基础属性() {
            baseProperties.Clear();
            Array values = Enum.GetValues(typeof(EnumPropertiesType));
            foreach (EnumPropertiesType property in values) {
                baseProperties.Add(new PropertiesStruct() {
                    propertiesType = property,
                    value = GameTools.RandomInt(10, 100)
                });
            }
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }
#endif
    }
}