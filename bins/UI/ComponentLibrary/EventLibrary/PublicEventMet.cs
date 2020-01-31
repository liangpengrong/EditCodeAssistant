using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Core.StaticMethod.Method.Utils;
using Core.DefaultData.DataLibrary;
using UI.ComponentLibrary.MethodLibrary.Util;

namespace Ui.ComponentLibrary.EventLibrary {
    /// <summary>
    /// 公共的事件绑定方法
    /// </summary>
    public class PublicEventMet {
        private PublicEventMet() { }
        /// <summary>
        /// 实例化文本选择对话框
        /// </summary>
        /// <param name="t">为null则打开一个新标签</param>
        /// <returns>该对话框</returns>
        public static object openFileMethod(TextBox t) {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.CheckFileExists = true;
            openFile.DefaultExt = "txt";
            openFile.Filter = "文本文档(*.txt)|*.txt|所有文件(*.*)|*.*";
            openFile.DereferenceLinks = true;
            openFile.Multiselect = true;
            openFile.RestoreDirectory = false;
            // 判断是否点击确定
            if (openFile.ShowDialog() == DialogResult.OK) {
                string path = openFile.FileName;
                // 判断编码
                Encoding encoding = FileUtils.GetType(path);
                TextBox tempTextB = t != null? t : MainTabControlUtils.GetNewPageTextBox();
                FileUtils.SetTextBoxValByPath(tempTextB, path, encoding);
            }
            return openFile;
        }

        /// <summary>
        /// 实例化文件保存对话框保存文本
        /// </summary>
        /// <param name="t">要保存内容的文本框</param>
        /// <returns></returns>
        public static object saveFileMethod(TextBox t) {
            SaveFileDialog newSaveFile = new SaveFileDialog();
            Dictionary<string,object> textTag = TextBoxUtils.GetTextTagToMap(t);
            newSaveFile.RestoreDirectory = false;
            newSaveFile.ValidateNames = true;
            newSaveFile.DefaultExt = "txt";
            if(t != null && t.Parent != null) { 
                newSaveFile.FileName = t.Parent.Text;
            }
            newSaveFile.Filter = "文本文档(*.txt)|*.txt|所有文件(*.*)|*.*";
            // 获取文本框保存的Ecoding
            Encoding encoding = Encoding.Default;
            if (textTag.ContainsKey(TextBoxTagKey.TEXTBOX_TAG_KEY_ECODING) && textTag[TextBoxTagKey.TEXTBOX_TAG_KEY_ECODING] is Encoding) { 
                encoding = (Encoding)textTag[TextBoxTagKey.TEXTBOX_TAG_KEY_ECODING];
            }
            //判断是否点击确定
            if (newSaveFile.ShowDialog() == DialogResult.OK) {
                string path = newSaveFile.FileName;
                // 调用方法写入文件内容
                FileUtils.FileWrite.WriteFile(path, t.Text, encoding);
                // 将保存路径加入到文本框的Tag属性
                TextBoxUtils.TextBoxAddTag(t, TextBoxTagKey.SAVE_FILE_PATH , newSaveFile.FileName);
                // 监听文件变化并弹窗提醒 传入的文本框为null则开启一个新标签
                TextBox tempTextB = t != null? t : MainTabControlUtils.GetNewPageTextBox();
                FileUtils.SetTextBoxValByPath(tempTextB, path, encoding);
            }
            return newSaveFile;
        }
        /// <summary>
        /// 实例化字体对话框
        /// </summary>
        /// <returns></returns>
        public static object fontDialogMethod(TextBox t) {
            Dictionary<string, object> retDic = new Dictionary<string, object>();
            FontDialog fontD = new FontDialog();
            fontD.AllowSimulations = true;
            fontD.AllowVectorFonts = true;
            fontD.AllowScriptChange = true;
            fontD.ScriptsOnly = true;
            fontD.Font = t.Font;
            fontD.ShowApply = true;
            fontD.ShowEffects = false;
            if(!retDic.ContainsKey("1")) retDic.Add("1", fontD);
            // 判断是否点击了应用
            fontD.Apply += (object sender, EventArgs e) =>{
                t.Font = fontD.Font;
                if(!retDic.ContainsKey("2")) retDic.Add("2", DialogResult.OK);
                
            };
            DialogResult dialogResult = fontD.ShowDialog();
            // 判断是否点击确定
            if(dialogResult == DialogResult.OK) {
                t.Font = fontD.Font;
                if(!retDic.ContainsKey("2"))  retDic.Add("2", DialogResult.OK);
            }
            return retDic;    
        }
    }
}
