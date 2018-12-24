using System;
using System.Text;
using ProSetUpForm;
using System.Windows.Forms;
using PubControlLibrary;
using System.Diagnostics;
using AllDllLoad;
using PubMethodLibrary;
using StaticDataLibrary;
using PubCacheArea;
using System.IO;
using System.Security.Permissions;
using System.Collections.Generic;

namespace CharsToolset
{
    /// <summary>
    /// TopMenu控件的委托类
    /// </summary>
    public class TopMenuEventMet
    {
        private TopMenuEventMet(){}
        /// <summary>
        /// 退出程序
        /// </summary>
        /// <returns></returns>
        public static object exitProgram(Dictionary<Type , object> data){ 
            FormUtislMet.dropOut();
            return null;
        }
        /// <summary>
        /// 实例化文本选择对话框
        /// </summary>
        /// <returns>返回该对话框</returns>
        public static object openFileMethod(Dictionary<Type , object> data)
        {
            // 获取文本框
            TextBox t = (TextBox)data[typeof(TextBox)];

            Encoding encoding = Encoding.Default;
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.CheckFileExists = true;
            openFile.DefaultExt = "txt";
            openFile.Filter = "文本文档(*.txt)|*.txt|所有文件(*.*)|*.*";
            openFile.DereferenceLinks = true;
            openFile.Multiselect = true;
            openFile.RestoreDirectory = false;
            if (openFile.ShowDialog() == DialogResult.OK)
            {//判断是否点击确定
                // 判断编码
                String[] pathArr = FileUtilsMet.getPathArr(openFile.FileName);
                if(!"txt".Equals(pathArr[2].ToLower())) { 
                    encoding = FileUtilsMet.isFileEncoding(openFile.FileName);
                }
                // 将文件内容赋值到文本框中
                t.Text = FileUtilsMet.FileRead.read(openFile.FileName, encoding);
                t.SelectionStart = t.TextLength;

                // 将文件路径写入到文本框tag数据中
                TextBoxUtilsMet.textAddTag(t, TextBoxTagKey.saveFilePath, openFile.FileName);
                // 将文件编码写入到文本框tag数据中
                TextBoxUtilsMet.textAddTag(t, TextBoxTagKey.textEcoding, encoding);
                // 监听文件变化并弹窗提醒
                PublicEventMet.monitorFileShowMess(openFile.FileName, t);
                t.Focus();
            }
            return null;
        }

        /// <summary>
        /// 实例化文件保存对话框保存文本
        /// </summary>
        /// <param name="t">要保存内容的文本框</param>
        /// <returns></returns>
        public static object saveFileMethod(Dictionary<Type , object> data)
        {
            // 获取文本框
            TextBox t = (TextBox)data[typeof(TextBox)];

            SaveFileDialog newSaveFile = new SaveFileDialog();
            newSaveFile.RestoreDirectory = false;
            newSaveFile.ValidateNames = true;
            newSaveFile.DefaultExt = "txt";
            newSaveFile.Filter = "文本文档(*.txt)|*.txt|所有文件(*.*)|*.*";
            newSaveFile.FileName = TextBoxUtilsMet.getDicTextTag(t)[TextBoxTagKey.fControlText].ToString();
            //判断是否点击确定
            if (newSaveFile.ShowDialog() == DialogResult.OK) {
                // 调用方法写入文件内容
                FileUtilsMet.FileWrite.writeFile(newSaveFile.FileName.ToString(), t.Text, Encoding.Default);
                // 将保存路径加入到文本框的Tag属性
                TextBoxUtilsMet.textAddTag(t, TextBoxTagKey.saveFilePath , newSaveFile.FileName.ToString());
                // 监听文件变化并弹窗提醒
                PublicEventMet.monitorFileShowMess(newSaveFile.FileName, t);
            }
            return null;
        }
        /// <summary>
        /// 判断文件应该是保存还是另存为
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static object saveOrSaveas(Dictionary<Type , object> data)
        {
            // 获取文本框
            TextBox t = (TextBox)data[typeof(TextBox)];

            object path = null;//定义初始化的路径
            TextBoxUtilsMet.getDicTextTag(t).TryGetValue(TextBoxTagKey.saveFilePath, out path);//赋值路径
            if (path!=null&&FileUtilsMet.isFileUrl(path.ToString()))//判断路径是否存在
            {
                if (path.ToString().Split('.')[path.ToString().Split('.').Length - 1].ToLower().Equals("txt"))
                {//判断文件后缀名是否为txt格式
                    if (MessageBox.Show(
                         "该文件为本地文件,确定要保存并覆盖吗？"
                         + System.Environment.NewLine
                         + "文件路径为：" + path
                        , "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        FileUtilsMet.FileWrite.writeFile(t.Tag.ToString()//调用方法写入文件内容
                            , t.Text//获取选中标签中的主文本框的值
                            , Encoding.Default);
                    }
                }
                else
                {
                    MessageBox.Show("只保存TXT格式的文件");
                }
            }
            else{
                saveFileMethod(data);
            }
            return null;
        }
        /// <summary>
        /// 实例化字体对话框
        /// </summary>
        /// <returns></returns>
        public static object fontDialogMethod(Dictionary<Type , object> data) {
            // 获取文本框
            TextBox t = (TextBox)data[typeof(TextBox)];

            FontDialog fontD = new FontDialog();
            fontD.AllowSimulations = true;
            fontD.AllowVectorFonts = true;
            fontD.AllowScriptChange = true;
            fontD.ScriptsOnly = true;
            fontD.Font = t.Font;
            fontD.ShowApply = true;
            fontD.ShowEffects = false;

            // 判断是否点击了应用
            fontD.Apply += (object sender, EventArgs e) =>{
               t.Font = fontD.Font; 
            };
            DialogResult dialogResult = fontD.ShowDialog();
            // 判断是否点击确定
            if(dialogResult == DialogResult.OK) {
               t.Font = fontD.Font;
            }
            return null;    
        }

        /// <summary>
        /// 实现用记事本打开文本框内容
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static object notepadOpenFile(Dictionary<Type , object> data)
        {
            // 获取文本框
            TextBox t = (TextBox)data[typeof(TextBox)];

            FileUtilsMet.turnOnNotepad(t.Text);
            return null;
        }

        /// <summary>
        /// 顶部菜单项的撤销文本框缓存
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object menuCancelTextBoxCache(Dictionary<Type , object> data) { 
            PublicEventMet.cancelTextBoxCache(data);
            return null;
        }
        /// <summary>
        /// 顶部菜单项的恢复文本框缓存
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object menuRestoreTextBoxCache(Dictionary<Type , object> data) { 
            PublicEventMet.restoreTextBoxCache(data);
            return null;
        }

        /// <summary>
        /// 打开设置窗口
        /// </summary>
        /// <param name="t">用于委托寻找方法的产数，实际方法里毫无意义</param>
        /// <returns></returns>
        public static object openSetUp(Dictionary<Type , object> data)
        {
            // 获取文本框
            TextBox t = (TextBox)data[typeof(TextBox)];

            SetUpMain setUpMain = new SetUpMain();
            setUpMain.Font = WinApiUtilsMet.GetWinDefaultFont();
            setUpMain.ShowDialog();
            return null;
        }
        /// <summary>
        /// 打开查找和替换窗口
        /// </summary>
        /// <returns></returns>
        public static object openFindAndReplace(Dictionary<Type , object> data)
        {
            // 获取文本框
            TextBox t = (TextBox)data[typeof(TextBox)];

            FindAndReplace findAndReplace = PublicEventMet.openFindAndReplace(t, true);
            return findAndReplace;
        }
        /// <summary>
        /// 打开分列窗口
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static object openSplitCharsForm(Dictionary<Type , object> data)
        {
            // 获取文本框
            TextBox t = (TextBox)data[typeof(TextBox)];

            SplitCharsForm splitChars = PublicEventMet.openSplitCharsForm(t, true);
            return splitChars;
        }
        /// <summary>
        /// 打开统计字符窗口
        /// </summary>
        /// <returns></returns>
        public static CharsStatistics openCharsStatistics(Dictionary<Type , object> data)
        {
            // 获取文本框
            TextBox t = (TextBox)data[typeof(TextBox)];

            CharsStatistics rowGoToForm = PublicEventMet.openCharsStatistics(t, false);
            rowGoToForm.ShowDialog();
            return null;
        }
        /// <summary>
        /// 打开转到行窗体
        /// </summary>
        /// <returns></returns>
        public static object openRowGoToForm(Dictionary<Type , object> data)
        {
            // 获取文本框
            TextBox t = (TextBox)data[typeof(TextBox)];

            RowGoToForm rowGoToForm = PublicEventMet.openRowGoToForm(t, false);
            rowGoToForm.ShowDialog();
            return rowGoToForm;
        }
        /// <summary>
        /// 实例化添加字符窗体
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object openAddCharsForm(Dictionary<Type , object> data) {
            // 获取文本框
            TextBox t = (TextBox)data[typeof(TextBox)];

            // 实例化窗体
            AddCharsForm addCharsForm = PublicEventMet.openAddCharsForm(t, true);
            return addCharsForm;
        }
        /// <summary>
        /// 实例化关于窗体
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object openThereofForm(Dictionary<Type , object> data) {

            // 实例化窗体
            ThereofForm thereofForm = new ThereofForm();
            thereofForm.ShowDialog();
            return null;
        }
        /// <summary>
        /// 设置是否自动换行
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object isAutoLine(Dictionary<Type , object> data) { 
            // 获取文本框
            TextBox t = (TextBox)data[typeof(TextBox)];
            ToolStripMenuItem item = (ToolStripMenuItem)data[typeof(ToolStripMenuItem)];
            // 设置状态栏显示与隐藏
            bool check = item.Checked;
            t.WordWrap = check;
            return null;
        }
        /// <summary>
        /// 设置状态栏的显示与隐藏
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object isStartBarDisplay(Dictionary<Type , object> data) {
            // 获取文本框
            TextBox t = (TextBox)data[typeof(TextBox)];
            ToolStripMenuItem item = (ToolStripMenuItem)data[typeof(ToolStripMenuItem)];
            // 全局单例控件工厂
            Dictionary<string, Control> single = ControlCache.getSingletonFactory();
            if(single.ContainsKey(DefaultNameCof.toolStart) && single.ContainsKey(DefaultNameCof.tabContent)) { 
                // 状态栏
                StatusStrip toolStrip = (StatusStrip)single[DefaultNameCof.toolStart];
                // 标签容器
                TabControl tabControl = (TabControl)single[DefaultNameCof.tabContent];
                // 设置状态栏显示与隐藏
                bool check = item.Checked;
                toolStrip.Visible = check;
                // 调整标签容器的位置
                if(check) { 
                    tabControl.Height = tabControl.Height - toolStrip.Height;
                } else { 
                    tabControl.Height = tabControl.Height + toolStrip.Height;
                }
            }
            return null;
        }
        
    }
}
