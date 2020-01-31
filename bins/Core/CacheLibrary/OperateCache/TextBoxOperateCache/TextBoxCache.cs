using Core.DefaultData.DataLibrary;
using Core.StaticMethod.Method.Utils;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Core.CacheLibrary.OperateCache.TextBoxOperateCache
{
    /// <summary>
    /// 文本框缓存类
    /// </summary>
    public static class TextBoxCache
    {
        /// <summary>
        /// 存放文本框缓存的缓存工厂
        /// </summary>
        private static Dictionary<string, List<TextBoxCacheModel>> cacheFactory = new Dictionary<string, List<TextBoxCacheModel>>();
        /// <summary>
        /// 当前缓存索引
        /// </summary>
        private static Dictionary<string, int> cacheIndexs = new Dictionary<string, int>();
        /// <summary>
        /// 将要操作的文本框
        /// </summary>
        public static TextBox UpOperatingTextBox { get; set; } = null;

        /// <summary>
        /// 将文本框的内容追加到缓冲区
        /// </summary>
        /// <param name="t">文本框</param>
        /// <param name="keys">文本框键盘数据</param>
        /// <param name="mous">文本框鼠标数据</param>
        public static void addCacheFactory(TextBox t) {
            // 判断是否需要记录到缓存缓存
            if( !isAddCasche(t)) return;
            // 记录文本框Name
            string key = t.Name;
            // 刷新索引
            refreshCacheIndex(t);
            // 将传入的文本框数据封装为缓存实体
            TextBoxCacheModel textM = creatTextModel(t);
            // 判断工厂中是否已经存在此文本框
            if(cacheFactory.ContainsKey(key)){
                List<TextBoxCacheModel> modelArr = containsKeyAddMet(textM);
                // 重新设置
                cacheFactory[key] = modelArr;
            } else {
                List<TextBoxCacheModel> modelArr = new List<TextBoxCacheModel>();
                modelArr.Add(TextBoxCacheModel.getDefaultModel(textM.TextBName));
                modelArr.Add(textM);
                cacheFactory.Add(key, modelArr);
            }
            // 同步缓存索引
            synchronizeIndex(t);
        }
        /// <summary>
        /// 当缓存工厂存在该文本框时,添加缓存的方式
        /// </summary>
        /// <param name="textM"></param>
        /// <returns></returns>
        private static List<TextBoxCacheModel> containsKeyAddMet(TextBoxCacheModel textM) { 
            string key = textM.TextBName;
            List<TextBoxCacheModel> retList = new List<TextBoxCacheModel>();
            // 得到该文本框对应的缓存实体集合
            cacheFactory.TryGetValue(key, out retList);
            // 判断缓存对象集合大小是否为0
            if(retList != null && !retList.Count.Equals(0)) {
                // 缓存添加策略
                retList = cacheAddStrategy(textM,retList);
            } else {
                // 直接添加
                retList.Add(textM);
            }
            return retList;
        }

        /// <summary>
        /// 判断需要记录到缓存中
        /// </summary>
        /// <param name="t"></param>
        /// <param name="keys"></param>
        /// <param name="mous"></param>
        /// <returns></returns>
        private static bool isAddCasche(TextBox t) {
            Dictionary<string, object> tag = TextBoxUtils.GetTextTagToMap(t);
            if(tag.ContainsKey(TextBoxTagKey.TEXTBOX_IS_CANCEL)) {
                bool cancel = (bool)tag[TextBoxTagKey.TEXTBOX_IS_CANCEL];
                // 判断处于撤销时不加入到缓存
                if(cancel) {
                    // 重置撤销状态
                    TextBoxUtils.TextBoxAddTag(t, TextBoxTagKey.TEXTBOX_IS_CANCEL, false);
                    return false;
                } 
                
            }
            if(tag.ContainsKey(TextBoxTagKey.TEXTBOX_EMPTY_NOT_CACHED)) {
                bool notCached = (bool)tag[TextBoxTagKey.TEXTBOX_EMPTY_NOT_CACHED];
                // 判断为空时不加入到缓存
                if(notCached && t.TextLength == 0) {
                    // 重置撤销状态
                    TextBoxUtils.TextBoxAddTag(t, TextBoxTagKey.TEXTBOX_EMPTY_NOT_CACHED, false);
                    return false;
                } 
                
            }
            if(tag.ContainsKey(TextBoxTagKey.TEXTBOX_IS_RESTORE)) { 
                bool restore = (bool)tag[TextBoxTagKey.TEXTBOX_IS_RESTORE];
                // 判断处于恢复时不加入到缓存
                if(restore) {
                    // 重置恢复状态
                    TextBoxUtils.TextBoxAddTag(t, TextBoxTagKey.TEXTBOX_IS_RESTORE, false);
                    return false;
                } 
            }
            return true;
        }
        /// <summary>
        /// 刷新缓存索引
        /// </summary>
        private static void refreshCacheIndex(TextBox t) {
            string key = t.Name;
            if(cacheFactory.ContainsKey(key) && cacheIndexs.ContainsKey(key)) {
                int index = cacheIndexs[key];
                List<TextBoxCacheModel> listMod = cacheFactory[key];
                if(index < listMod.Count) {
                    for(int i = listMod.Count -1; i> index ;i--) { 
                        listMod.RemoveAt(i);
                    }
                    cacheFactory[key] = listMod;
                }
                if(index > cacheFactory[key].Count) { 
                    cacheIndexs[key] = cacheFactory[key].Count;
                }
                
            }
            
        }
        /// <summary>
        /// 将索引同步为缓存List中的索引
        /// </summary>
        /// <returns></returns>
        private static int synchronizeIndex(TextBox t) {
            int index = 0;
            string key = t.Name;
            if(cacheFactory.ContainsKey(key) && cacheFactory[key] != null) {
                List<TextBoxCacheModel> listMod = cacheFactory[key];
                index = listMod.Count == 0? 0 : listMod.Count - 1;
                if(cacheIndexs.ContainsKey(key)) {
                    // 覆盖
                    cacheIndexs[key] = index;
                } else {
                    // 添加入缓存工厂
                    cacheIndexs.Add(key, index);  
                }
            }
            return index;
        }
        /// <summary>
        /// 生成缓存实体类
        /// </summary>
        /// <param name="t">文本框</param>
        /// <param name="keys">文本框的键盘事件</param>
        /// <param name="mous">文本框的鼠标事件</param>
        /// <returns></returns>
        private static TextBoxCacheModel creatTextModel(TextBox t){ 
            TextBoxCacheModel textM = new TextBoxCacheModel();
            string key = t.Name;
            textM.TextBName = t.Name;
            // 创建时间
            textM.CreateTime = DateTime.Now.ToString();
            // 判断选择起始位置
            textM.SelectStart = selectStartStrategy(t);
            // 选中长度
            textM.SelectLegth = t.SelectionLength;
            
            // 文本内容
            textM.Text = t.Text;
            
            // 判断缓存类型
            if(cacheFactory.ContainsKey(key) && cacheFactory[key].Count > 0) {
                List<TextBoxCacheModel> modelArr = cacheFactory[key];
                TextBoxCacheModel upMod = modelArr[modelArr.Count - 1];
                textM.TextType = cacheTypeStrategy(t, upMod);
            } else{
                textM.TextType = cacheTypeStrategy(t, null);
            }
            return textM;
        }
    /*=================================策略生成区域===========================*/
        /// <summary>
        /// 缓存对象添加到缓存List集合时的添加策略
        /// </summary>
        /// <param name="textM"></param>
        /// <param name="modList"></param>
        /// <returns></returns>
        private static List<TextBoxCacheModel> cacheAddStrategy(TextBoxCacheModel textM, List<TextBoxCacheModel> modList){ 
            // 获取上一个缓存对象
            TextBoxCacheModel upMod = modList[modList.Count - 1];
            // 判断是否删除了全部的文本
            if(textM.Text.Length.Equals(0)) { 
                modList.Add(textM);
                return modList;
            }

            // 判断是否输入了不同语言
            if ( (StringUtils.IsStrChines(textM.Text) && !StringUtils.IsStrChines(upMod.Text))
                || !StringUtils.IsStrChines(textM.Text) && StringUtils.IsStrChines(upMod.Text) ) { 
                    
                modList.Add(textM);
                return modList;
            }
            /*======================删除判断===========================*/
            if(textM.Text.Length + 1 == upMod.Text.Length
                && upMod.SelectStart != upMod.Text.Length
                && upMod.Text.Substring(upMod.SelectStart == 0?0:upMod.SelectStart -1,1)
                .Equals(upMod.Text.Substring(upMod.SelectStart,1))) {
                
                modList[modList.Count - 1] = textM;
                return modList;
            }
            // 判断鼠标左键或者右键按下
            if (WinApiUtils.GetAsyncKeyState(0x02) > 0|| WinApiUtils.GetAsyncKeyState(0x01) > 0) {
                modList.Add(textM);
                return modList;
            } else {
                if(textM.Text == null || textM.Text.Equals("")) { 
                    return modList;
                }    
            }
            ///*======================按键判断===========================*/
            // 判断键盘按键是否和上一个缓存一样并且不是按下了ctrl+v, delete
            //if (textM.KeysEvent.KeyCode.Equals(upMod.KeysEvent.KeyCode)
            //&& !(textM.KeysEvent.Control && textM.KeysEvent.KeyCode.Equals(Keys.V))) {
            //    modList[modList.Count - 1] = textM;
            //    return modList;
            //}
            modList.Add(textM);
            return modList;
        }

        /// <summary>
        /// 缓存类型生成策略
        /// </summary>
        /// <param name="t">文本框</param>
        /// <param name="upMod">上一个缓存对象</param>
        /// <returns></returns>
        private static TextCacheTypeEnum cacheTypeStrategy(TextBox t, TextBoxCacheModel upMod) {
            if(upMod == null) return TextCacheTypeEnum.插入;
            string text = t.Text;
            string upText = upMod.Text;
            if(null == upText || "".Equals(upText)) { 
                return TextCacheTypeEnum.插入;
            }
            if(text.Length > upText.Length) { 
                return TextCacheTypeEnum.插入;
            }
            if(text.Length < upText.Length) { 
                return TextCacheTypeEnum.删除;
            }
            if(text.Length.Equals(upText.Length) && !text.Equals(upText)) { 
                return TextCacheTypeEnum.修改;
            }
            return TextCacheTypeEnum.NONE;
        }
        /// <summary>
        /// 选择起始位置生成策略
        /// </summary>
        /// <param name="t"></param>
        /// <param name="keys"></param>
        /// <param name="mous"></param>
        /// <returns></returns>
        private static int selectStartStrategy(TextBox t) { 
            int retInt = t.SelectionStart;
            
            return retInt;
        }
    /*=================================策略生成区域===========================*/
    /*=================================策略生成区域===========================*/
    /*=================================策略生成区域===========================*/



        /// <summary>
        /// 撤销缓存区域
        /// </summary>
        /// <param name="t">要撤销的文本框</param>
        public static void cancelCache(TextBox t) {
            t.ClearUndo();
            string key = t.Name;
            if(cacheFactory.ContainsKey(key) && cacheIndexs.ContainsKey(key)) {
                int index = cacheIndexs[key];
                List<TextBoxCacheModel> listMod = cacheFactory[key];
                TextBoxCacheModel tMode = null;
                if(index - 1 >= 0) {
                    index --;
                    tMode = listMod[index];
                    // 文本内容
                    t.Text = tMode.Text;
                    // 选中起始位置
                    t.SelectionStart = tMode.SelectStart;
                    // 选中长度
                    t.SelectionLength = tMode.SelectLegth;
                    // 刷新索引
                    cacheIndexs[key] = index;
                } else { 
                    cacheIndexs[key] = 0;
                    t.Text = "";
                    // 选中起始位置
                    t.SelectionStart = 0;
                    //MessageBox.Show("缓存区索引已到达开始");
                }
            }
            t.ScrollToCaret();
        }
        /// <summary>
        /// 恢复缓存区域
        /// </summary>
        /// <param name="t"></param>
        public static void restoreCache(TextBox t) {
            t.ClearUndo();
            string key = t.Name;
            if(cacheFactory.ContainsKey(key) && cacheIndexs.ContainsKey(key)) {
                int index = cacheIndexs[key];
                List<TextBoxCacheModel> listMod = cacheFactory[key];
                TextBoxCacheModel tMode = null;
                if(index < listMod.Count - 1) { 
                    index ++;
                    tMode = listMod[index];
                    
                } else {
                    tMode = listMod[listMod.Count - 1];
                    //MessageBox.Show("缓存区索引已到达末尾");    
                }
                // 文本内容
                t.Text = tMode.Text;
                // 选中起始位置
                t.SelectionStart = tMode.SelectStart;
                // 选中长度
                t.SelectionLength = tMode.SelectLegth;
                // 刷新索引
                cacheIndexs[key] = index;
            }
            t.ScrollToCaret();
        }
        /// <summary>
        /// 获取文本框缓存工厂
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string,List<TextBoxCacheModel>> getCacheFactory(){
            return cacheFactory;
        }

    }
}
